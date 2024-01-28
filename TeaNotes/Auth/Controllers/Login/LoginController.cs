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
            var user = await _db.Users.FirstOrDefaultAsync(u => u.NickName == payload.NickName);

            if (user is null)
            {
                return NotFound("User is not found");
            }

            if (!IsPasswordCorrect(payload.Password, user.PasswordHash))
            {
                return BadRequest("Password isn't correct");
            }

            await _db.RefreshSessions.Where(s => s.UserId == user.Id).ExecuteDeleteAsync();
            await _db.SaveChangesAsync();

            

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
            var (refreshToken, refreshExpiresAt) = _jwtTokenGenerator.GenerateRefreshToken();
            await _db.RefreshSessions.AddAsync(new() { 
                UserId = user.Id, 
                RefreshToken = refreshToken, 
                ExpiresAt = refreshExpiresAt,
            });

            Response.Cookies.Append(CookieKeys.RefreshToken, refreshToken, new() { 
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None, 
                Expires = refreshExpiresAt,
                Domain = "localhost",
            });
            Response.Cookies.Append("User-Id", user.Id.ToString());

            (var accessToken, var accessExpiresAt) = _jwtTokenGenerator.GenerateAccessToken(user);
            await _db.SaveChangesAsync();

            return Ok(new LoginResponse()
            {
                
                User = user,
                Tokens = new()
                {
                    Access = new() { Token = accessToken, ExpiresAt = accessExpiresAt},
                    Refresh = new () { Token = refreshToken, ExpiresAt = refreshExpiresAt },
                }
            });
        }
    }
}
