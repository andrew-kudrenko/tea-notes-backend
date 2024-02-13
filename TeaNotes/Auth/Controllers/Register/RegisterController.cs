using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly RegisterEmailBuilder _registerEmailBuilder;
        private readonly ConfirmationCodeGenerator _confirmationCodeGenerator;

        public RegisterController(
            AppDbContext db, 
            EmailClient emailClient, 
            RegisterEmailBuilder registerEmailBuilder,
            ConfirmationCodeGenerator confirmationCodeGenerator
        )
        {
            _db = db;
            _emailClient = emailClient;
            _registerEmailBuilder = registerEmailBuilder;
            _confirmationCodeGenerator = confirmationCodeGenerator;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] RegisterPayload payload)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.NickName == payload.NickName);

            if (user is not null)
            {
                return Conflict("User with that nickname already exists");
            }

            var added = await _db.Users.AddAsync(new() { 
                NickName = payload.NickName, 
                PasswordHash = HashPassword(payload.Password),
                Email = payload.Email,
                IsEmailVerified = false,
            });
            await _db.SaveChangesAsync();

            await SendEmailConfirmationAsync(added.Entity);

            return StatusCode(StatusCodes.Status201Created, added.Entity);
        }

        private static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        private async Task SendEmailConfirmationAsync(User user)
        {
            var code = _confirmationCodeGenerator.Generate(user);

            await _db.RegisterConfirmationCodes.AddAsync(code);
            await _db.SaveChangesAsync();

            await _emailClient.SendAsync(_registerEmailBuilder.Create(user, code));
        }
    }
}
