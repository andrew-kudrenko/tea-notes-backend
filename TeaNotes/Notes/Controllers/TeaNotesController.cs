using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaNotes.Database;
using TeaNotes.Notes.Controllers.Dto;

namespace TeaNotes.Notes.Controllers
{
    [Route("api/notes")]
    public class TeaNotesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public TeaNotesController(AppDbContext db) => _db = db;

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(
                _db.TeaNotes
                    .Include(n => n.Tastes)
                    .Include(n => n.User)
                    .Select(TeaNoteDto.FromTeaNote)
                    .ToList()
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeaNoteDto source) 
        {
            if (int.TryParse(Request.Cookies["User-Id"], out var userId))
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user is null)
                {
                    return BadRequest("User is not defined");
                }

                var note = source.ToTeaNote();
                note.User = user;

                var created = await _db.TeaNotes.AddAsync(note);
                await _db.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, created.Entity);
            }

            return BadRequest();
        }
    }
}
