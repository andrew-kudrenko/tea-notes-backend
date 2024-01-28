using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaNotes.Auth.Utility;
using TeaNotes.Database;
using TeaNotes.Notes.Controllers.Dto;
using TeaNotes.Users.Models;

namespace TeaNotes.Notes.Controllers
{
    [Authorize]
    [Route("api/notes")]
    public class TeaNotesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public TeaNotesController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var user = await GetUser();

            if (user is null)
            {
                return BadRequest("User is not defined");
            }

            return Ok(
                _db.TeaNotes
                    .Include(n => n.Tastes)
                    .Include(n => n.User)
                    .Where(n => n.User.Id == user.Id)
                    .Select(TeaNoteDto.FromTeaNote)
                    .ToList()
            );
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var note = await _db.TeaNotes
                .Include(n => n.Tastes)
                .Include(n => n.User)
                .FirstOrDefaultAsync(n => n.Id == id);

            return note is not null ? Ok(TeaNoteDto.FromTeaNote(note)) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await GetUser();

            if (user is not null)
            {
                var note = await _db.TeaNotes.FirstOrDefaultAsync(n => n.Id == id);

                if (note is not null && note.User.Id == user.Id)
                {
                    _db.TeaNotes.Remove(note);
                    await _db.SaveChangesAsync();

                    return NoContent();
                }
            }

            return BadRequest("User is not defined");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeaNoteDto source) 
        {
            var user = await GetUser();

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] TeaNoteDto source, int id)
        {
            var foundNote = await _db.TeaNotes.FirstOrDefaultAsync(n => n.Id == id);

            if (foundNote is null)
            {
                return BadRequest($"Note is not found by id {id}");
            }

            var user = await GetUser();
            
            if (user is null)
            {
                return BadRequest("User is not defined");
            }
            
            source.AssignToTeaNote(foundNote);
            await _db.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, foundNote);
        }

        private async Task<User?> GetUser()
        {
            return int.TryParse(Request.Cookies[CookieKeys.UserId], out var userId)
                ? await _db.Users.FirstOrDefaultAsync(u => u.Id == userId)
                : null;
        }
    }
}
