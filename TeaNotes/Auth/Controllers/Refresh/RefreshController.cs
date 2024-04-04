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
            var requestRefreshToken = Request.Cookies[AuthCookieKeys.RefreshToken];

            if (requestRefreshToken is null)
            {
                return Unauthorized(Resources.ErrorMessages.RefreshTokenNotFound);
            }

            var session = await _db.RefreshSessions.FirstOrDefaultAsync(s => s.RefreshToken == requestRefreshToken);

            if (session is { })
            {
                await _db.RefreshSessions.Where(s => s.Id == session.Id).ExecuteDeleteAsync();
                await _db.SaveChangesAsync();
            } 
            else 
            {
                return Unauthorized(Resources.ErrorMessages.RefreshSessionNotFound);
            }

            if (session.ExpiresAt <= DateTime.Now)
            {
                return Unauthorized(Resources.ErrorMessages.OutdatedRefreshToken);
            }

            var refresh = _jwtTokenGenerator.GenerateRefreshToken();
            Response.Cookies.Append(AuthCookieKeys.RefreshToken, refresh.Token, new()
            {
                HttpOnly = true,
                Secure = true,
                Expires = refresh.ExpiresAt,
            });

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == session.UserId);

            if (user is null)
            {
                return Unauthorized(Resources.ErrorMessages.UserNotFound);
            }

            await _db.RefreshSessions.AddAsync(new()
            {
                UserId = user.Id,
                RefreshToken = refresh.Token,
                ExpiresAt = refresh.ExpiresAt,
            });

            var access = _jwtTokenGenerator.GenerateAccessToken(user);

            await _db.SaveChangesAsync();

            return Ok(new RefreshResponse(access));
        }
    }
}
