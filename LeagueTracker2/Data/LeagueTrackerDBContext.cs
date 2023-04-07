using Microsoft.EntityFrameworkCore;

namespace LeagueTracker2.Data
{
    public class LeagueTrackerDBContext : DbContext
    {
        public LeagueTrackerDBContext(DbContextOptions<LeagueTrackerDBContext> options)
            : base(options)
        {
        }

        // Define your entities as DbSet properties here
        //public DbSet<YourEntity> YourEntities { get; set; }
    }
}
