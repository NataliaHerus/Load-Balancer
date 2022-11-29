using Microsoft.EntityFrameworkCore;

namespace LoadBalancer.EntityFramework
{
    public class Listener
    {
        public int Id { get; set; }
        
        public int Port { get; set; }

        public int Load { get; set; }
    }

    public class LoadBalancerDbContext : DbContext
    {
        public DbSet<Listener> Listeners { get; set; }

        public LoadBalancerDbContext(DbContextOptions<LoadBalancerDbContext> options)
            : base(options)
        {

        }
    }
}
