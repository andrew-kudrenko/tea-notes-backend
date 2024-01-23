using Microsoft.EntityFrameworkCore;
using TeaNotes.Auth.Models;
using TeaNotes.Notes.Models;

namespace TeaNotes.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User.Models.User> Users { get; set; }
        public DbSet<RefreshSession> RefreshSessions { get; set; }
        public DbSet<TeaNote> TeaNotes { get; set; }
        public DbSet<TeaTaste> TeaTastes { get; set; }
        public AppDbContext(DbContextOptions options) : base(options) { }
    }
}
