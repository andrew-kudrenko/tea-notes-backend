using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaNotes.Database;

namespace TeaNotes.User.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db) => _db = db;

        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _db.Users.ToListAsync());
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<Models.User>> GetMe() 
        {
            return User.Identity is null 
                ? NotFound("Me is nothing") 
                : Ok(await _db.Users.FirstOrDefaultAsync(u => u.NickName == User.Identity.Name));
        }
    }
}
