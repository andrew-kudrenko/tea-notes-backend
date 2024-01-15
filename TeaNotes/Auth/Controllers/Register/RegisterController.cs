using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaNotes.Database;

namespace TeaNotes.Auth.Controllers.Register
{
    [Route("api/auth/register")]
    public class RegisterController : ControllerBase
    {
        private readonly AppDbContext _db;

        public RegisterController(AppDbContext db) => _db = db;

        [HttpPost]
        public async Task<ActionResult<User.Models.User>> Register([FromBody] RegisterPayload payload)
        {
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

            return Ok(added.Entity);
        }

        private static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    }
}
