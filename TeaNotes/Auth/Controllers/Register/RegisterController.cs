using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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
            var newUser = new User()
            {
                Nickname = payload.Nickname,
                PasswordHash = HashPassword(payload.Password),
                Email = payload.Email,
                IsEmailVerified = false,
                RegisteredAt = DateTime.Now,
            };
            var validationContext = new ValidationContext(newUser);

            if (!TryValidateModel(newUser))
            {
                var errorList = ModelState
                    .Where(f => f.Value!.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ElementAt(0)
                    );

                return BadRequest(new { errors = errorList });
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Nickname == payload.Nickname || u.Email == payload.Email);

            if (user is not null)
            {
                return Conflict(new
                {
                    Errors = new Dictionary<string, string>()
                    {
                        { "email", user.Email == payload.Email ? "Пользователь с таким адресом эл.почтчы уже зарегистрирован" : "" },
                        { "nickname", user.Nickname == payload.Nickname ? "Пользователь с таким ником уже зарегистрирован" : "" },
                    },
                });
            }

            var added = await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();

            await SendEmailConfirmationAsync(added.Entity);

            return StatusCode(StatusCodes.Status201Created, new { user = added.Entity });
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
