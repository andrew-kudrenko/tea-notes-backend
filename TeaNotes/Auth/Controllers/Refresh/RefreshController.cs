using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaNotes.Auth.Jwt;
using TeaNotes.Auth.Utility;
using TeaNotes.Database;

namespace TeaNotes.Auth.Controllers.Refresh
{
    [Route("api/auth/refresh")]
    public class RefreshController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public RefreshController(AppDbContext db, JwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost]
        public async Task<ActionResult<RefreshResponse>> RefreshTokens()
        {
            var requestRefreshToken = Request.Cookies[CookieAuthKeys.RefreshToken];

            if (requestRefreshToken is null)
            {
                return BadRequest("Refresh token wasn't provided");
            }

            var session = await _db.RefreshSessions.FirstOrDefaultAsync(s => s.RefreshToken == requestRefreshToken);

            if (session is { })
            {
                await _db.RefreshSessions.Where(s => s.Id == session.Id).ExecuteDeleteAsync();
            } 
            else 
            {
                return BadRequest("Session not found");
            }

            if (session.ExpiresAt <= DateTime.Now)
            {
                return BadRequest("Refresh token is expired");
            }

            var (refreshToken, refreshExpiresAt) = _jwtTokenGenerator.GenerateRefreshToken();
            Response.Cookies.Append(CookieAuthKeys.RefreshToken, refreshToken, new()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = refreshExpiresAt,
                Domain = "localhost",
            });

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == session.UserId);

            if (user is null)
            {
                return BadRequest("User is not found");
            }

            await _db.RefreshSessions.AddAsync(new()
            {
                UserId = user.Id,
                RefreshToken = refreshToken,
                ExpiresAt = refreshExpiresAt,
            });

            var (accessToken, accessExpiresAt) = _jwtTokenGenerator.GenerateAccessToken(user);

            await _db.SaveChangesAsync();

            return Ok(new RefreshResponse()
            {
                Tokens = new()
                {
                    Access = new () { Token = accessToken, ExpiresAt = accessExpiresAt },
                    Refresh = new () { Token = refreshToken, ExpiresAt = refreshExpiresAt},
                }
            });
        }
    }
}
