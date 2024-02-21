using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaNotes.Auth.Jwt;
using TeaNotes.Auth.Utility;
using TeaNotes.Database;
using TeaNotes.Users.Models;

namespace TeaNotes.Auth.Controllers.Login
{
    [Route("api/auth")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public LoginController(AppDbContext db, JwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginPayload payload)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Nickname == payload.Nickname);

            if (user is null)
            {
                return NotFound("User is not found");
            }

            if (!IsPasswordCorrect(payload.Password, user.PasswordHash))
            {
                return BadRequest("Password isn't correct");
            }

            if (!user.IsEmailVerified)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Адрес эл.почты не подтверждён");
            }

            return await CreateResponse(user);
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            if (Request.Cookies.ContainsKey(CookieKeys.UserId))
            {
                var userId = int.Parse(Request.Cookies[CookieKeys.UserId]!);

                await _db.RefreshSessions.Where(s => s.UserId == userId).ExecuteDeleteAsync();
                await _db.SaveChangesAsync();
            }

            Response.Cookies.Delete(CookieKeys.RefreshToken);
            Response.Cookies.Delete(CookieKeys.UserId);

            return NoContent();
        }

        private static bool IsPasswordCorrect(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);

        private async Task<ActionResult<LoginResponse>> CreateResponse(User user)
        {
            var refresh = _jwtTokenGenerator.GenerateRefreshToken();
            
            await _db.RefreshSessions.AddAsync(new() { 
                UserId = user.Id, 
                RefreshToken = refresh.Token, 
                ExpiresAt = refresh.ExpiresAt,
            });
            await _db.SaveChangesAsync();

            var secureCookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = refresh.ExpiresAt,
                Domain = "localhost",
            };

            Response.Cookies.Append(CookieKeys.RefreshToken, refresh.Token, secureCookieOptions);
            Response.Cookies.Append(CookieKeys.UserId, user.Id.ToString(), secureCookieOptions);

            return Ok(new LoginResponse()
            {
                User = user,
                Tokens = new() { 
                    Access = _jwtTokenGenerator.GenerateAccessToken(user), 
                    Refresh = refresh,
                }
            });
        }
    }
}
