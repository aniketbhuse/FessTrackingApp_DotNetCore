using FeesTrackingApplication.Models;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry;

namespace FeesTrackingApplication.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Students> Students { get; set; }

        public DbSet<Batches> Batches { get; set; }
    }
}
