using LoadBalancer.WebApi.Data.Entities.StateEntity;
using LoadBalancer.WebApi.Data.Entities.PuzzleEntity;
using Microsoft.EntityFrameworkCore;
using LoadBalancer.WebApi.Data.Entities.UzerToPuzzleEntity;
using LoadBalancer.WebApi.Data.SeedData;
using LoadBalancer.WebApi.Data.Entities.ListenerEntity;

namespace LoadBalancer.WebApi.Data
{
    public class LoadBalancerDbContext : DbContext
    {
        public DbSet<Puzzle> Puzzles { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<UserToPuzzle> UserToPuzzle { get; set; }
        public DbSet<Listener> Listeners { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PuzzleConfiguration());
            modelBuilder.ApplyConfiguration(new StateConfiguration());
            modelBuilder.ApplyConfiguration(new UserToPuzzleConfuguration());
            modelBuilder.ApplyConfiguration(new ListenerConfiguration());

            modelBuilder.Seed();
        }
        public LoadBalancerDbContext(DbContextOptions<LoadBalancerDbContext> options)
               : base(options)
        {
        }
    }
}
