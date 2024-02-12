using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using TeaNotes.Database;
using TeaNotes.Email;
using TeaNotes.Users.Models;

namespace TeaNotes.Auth.Controllers.Register
{
    [Route("api/auth/register")]
    public class RegisterController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly EmailClient _emailClient;

        public RegisterController(AppDbContext db, EmailClient emailClient)
        {
            _db = db;
            _emailClient = emailClient;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] RegisterPayload payload)
        {
            await _emailClient.SendAsync(CreateConfirmationMessage(payload));

            var user = await _db.Users.FirstOrDefaultAsync(u => u.NickName == payload.NickName);

            if (user is not null)
            {
                return Conflict("User with that nickname already exists");
            }

            var added = await _db.Users.AddAsync(new() { 
                NickName = payload.NickName, 
                PasswordHash = HashPassword(payload.Password),
            });
            await _db.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, added.Entity);
        }

        private static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        private MimeMessage CreateConfirmationMessage(RegisterPayload payload)
        {
            var bodyBuilder = new BodyBuilder
            {
                TextBody = $"Hello, {payload.NickName}! Please confirm the code! Your code: {Guid.NewGuid().ToString()}"
            };

            var message = new MimeMessage
            {
                Body = bodyBuilder.ToMessageBody(),
                Subject = "Confirm signing up",
                Sender = _emailClient.Address,
            };

            message.From.Add(_emailClient.Address);
            message.To.Add(MailboxAddress.Parse(payload.Email));

            return message;
        }
    }
}
