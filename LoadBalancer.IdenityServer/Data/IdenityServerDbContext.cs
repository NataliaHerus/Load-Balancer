using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LoadBalancer.IdenityServer.Data.Models;

namespace LoadBalancer.IdenityServer.Data
{
    public class IdenityServerDbContext : IdentityDbContext<User>
    {
        public IdenityServerDbContext(DbContextOptions<IdenityServerDbContext> options)
            : base(options)
        {
        }
    }
}