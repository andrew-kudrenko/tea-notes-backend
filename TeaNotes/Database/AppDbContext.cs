using Microsoft.EntityFrameworkCore;
using TeaNotes.Auth.Models;
using TeaNotes.Email;
using TeaNotes.Notes.Models;
using TeaNotes.Users.Models;

namespace TeaNotes.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshSession> RefreshSessions { get; set; }
        public DbSet<TeaNote> TeaNotes { get; set; }
        public DbSet<TeaTaste> TeaTastes { get; set; }
        public DbSet<ConfirmationCode> RegisterConfirmationCodes { get; set; }
        public AppDbContext(DbContextOptions options) : base(options) { }
    }
}
