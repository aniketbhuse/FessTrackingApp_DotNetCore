using FeesTrackingApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FeesTrackingApplication.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Students> Students { get; set; }
    }
}
