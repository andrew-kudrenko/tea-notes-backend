﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaNotes.Database;

namespace TeaNotes.Users.Controllers
{
    [Authorize]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db) => _db = db;

        public async Task<ActionResult> GetAll()
        {
            return Ok(await _db.Users.ToListAsync());
        }

        [HttpGet("me")]
        public async Task<ActionResult<Models.User>> GetMe() 
        {
            return User.Identity is null 
                ? NotFound("Me is nothing") 
                : Ok(await _db.Users.FirstOrDefaultAsync(u => u.Nickname == User.Identity.Name));
        }
    }
}
