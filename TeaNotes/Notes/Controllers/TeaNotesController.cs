﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaNotes.Auth.Utility;
using TeaNotes.Database;
using TeaNotes.Notes.Controllers.Dto;
using TeaNotes.Notes.Models;
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
                QueryNotes()
                    .Where(n => n.User.Id == user.Id)
                    .Select(TeaNoteDto.FromTeaNote)
                    .ToList()
            );
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var note = await QueryNotes().FirstOrDefaultAsync(n => n.Id == id);

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
            if (source is null)
            {
                return BadRequest(source);
            }

            var user = await GetUser();

            if (user is null)
            {
                return BadRequest("User is not defined");
            }

            var note = await CreateTeaNote(source, user);
            var created = await _db.TeaNotes.AddAsync(note);
            await _db.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, await FindNoteById(created.Entity.Id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] TeaNoteDto source, int id)
        {
            var foundNote = await FindNoteById(id);

            if (foundNote is null)
            {
                return BadRequest($"Note is not found by id {id}");
            }

            var user = await GetUser();
            
            if (user is null)
            {
                return BadRequest("User is not defined");
            }
            
            await AssignTeaNote(foundNote, source);
            foundNote = await FindNoteById(id);

            if (foundNote is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return StatusCode(StatusCodes.Status201Created, TeaNoteDto.FromTeaNote(foundNote));
        }

        private async Task<User?> GetUser()
        {
            return int.TryParse(Request.Cookies[CookieKeys.UserId], out var userId)
                ? await _db.Users.FirstOrDefaultAsync(u => u.Id == userId)
                : null;
        }

        private async Task<TeaNote?> FindNoteById(int? id) => await QueryNotes().FirstOrDefaultAsync(n => n.Id == id);

        private IQueryable<TeaNote> QueryNotes() => _db.TeaNotes.Include(n => n.Tastes).Include(n => n.User);

        private async Task<TeaNote> CreateTeaNote(TeaNoteDto source, User user)
        {
            var note = new TeaNote { User = user };
            await AssignTeaNote(note, source);
            return note;
        }

        private async Task AssignTeaNote(TeaNote dest, TeaNoteDto source)
        {
            if (dest.Id is not null)
            {
                await _db.TeaTastes.Where(t => t.TeaNoteId == dest.Id).ExecuteDeleteAsync();
                await _db.SaveChangesAsync();
            }

            await _db.TeaTastes.AddRangeAsync(source.Infusion.Tastes.Select(t => new TeaTaste() { Kind = t, TeaNote = dest }).ToArray());
            await _db.SaveChangesAsync();

            dest.Title = source.General.Title;
            dest.Kind = source.General.Kind;
            dest.Region = source.General.Region;
            dest.Manufacturer = source.General.Manufacturer;
            dest.ManufacturingYear = source.General.ManufacturingYear;
            dest.PricePerGram = source.General.PricePerGram;
            dest.TastingDate = source.General.TastingDate;

            dest.BrewingDishware = source.Brewing.Dishware;
            dest.BrewingMethod = source.Brewing.Method;
            dest.BrewingQuantity = source.Brewing.Quantity;
            dest.BrewingTemperature = source.Brewing.Temperature;
            dest.BrewingVolume = source.Brewing.Volume;

            dest.DryLeafAppearance = source.DryLeaf.Appearance;
            dest.DryLeafAroma = source.DryLeaf.Aroma;

            dest.InfusionAppearance = source.Infusion.Appearance;
            dest.InfusionAroma = source.Infusion.Aroma;
            dest.InfusionBalance = source.Infusion.Balance;
            dest.InfusionBouquet = source.Infusion.Bouquet;
            dest.InfusionDensity = source.Infusion.Density;
            dest.InfusionExtractivity = source.Infusion.Extractivity;
            dest.InfusionTartness = source.Infusion.Tartness;
            dest.InfusionTaste = source.Infusion.Taste;
            dest.InfusionViscosity = source.Infusion.Viscosity;

            dest.AftertasteComment = source.Aftertaste.Comment;
            dest.AftertasteDuration = source.Aftertaste.Duration;
            dest.AftertasteIntensity = source.Aftertaste.Intensity;

            dest.ImpressionComment = source.Impression.Comment;
            dest.ImpressionRate = source.Impression.Rate;
            dest.ImpressionWellCombinedWith = source.Impression.WellCombinedWith;
        }
    }
}
