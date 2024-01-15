using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaNotes.Database;
using TeaNotes.Notes.Controllers.Dto;
using TeaNotes.Notes.Models;

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
            return Ok(_db.TeaNotes.Include(n => n.Tastes).Include(n => n.User).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeaNoteDto source) 
        {
            if (int.TryParse(Request.Cookies["User-Id"], out var userId))
            {
                var note = await _db.TeaNotes.AddAsync(new()
                {
                    Aroma = source.Aroma,
                    Feeling = source.Feeling,
                    Taste = source.Taste,
                    Temperature = source.Temperature,
                    Title = source.Title,
                    User = await _db.Users.FirstAsync(u => u.Id == userId),
                    Tastes = source.Tastes.Select(t => new TeaNoteTaste() { Kind = t }).ToList(),        
                });
                await _db.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, note.Entity);
            }

            return BadRequest();
        }
    }
}
