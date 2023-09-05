using Microsoft.EntityFrameworkCore;
using TeaNotes.Auth.Models;

namespace TeaNotes.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User.Models.User> Users { get; set; }
        public DbSet<RefreshSession> RefreshSessions { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }
    }
}
