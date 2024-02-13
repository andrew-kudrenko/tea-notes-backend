using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaNotes.Database;

namespace TeaNotes.Auth.Controllers.EmailConfirmation
{
    [Route("api/auth/confirm-email")]
    public class EmailConfirmationController : ControllerBase
    {
        private readonly AppDbContext _db;

        public EmailConfirmationController(AppDbContext db) => _db = db;

        [HttpGet("{code}")]
        public async Task<ActionResult> Confirm(string code)
        {
            var foundCode = await _db.RegisterConfirmationCodes
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Code == code);

            if (foundCode is null)
            {
                return NotFound();
            }

            if (DateTime.Now > foundCode.ExpiresAt)
            {
                return BadRequest("Code is outdated");
            }

            foundCode.User.IsEmailVerified = true;
            await _db.RegisterConfirmationCodes
                .Where(c => c.UserId == foundCode.UserId)
                .ExecuteDeleteAsync();
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
