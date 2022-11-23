using LoadBalancer.WebApi.Data;
using LoadBalancer.WebApi.Data.Entities.StateEntity;
using LoadBalancer.WebApi.Repositories.Interfaces;

namespace LoadBalancer.WebApi.Repositories
{
    public class StateRepository : IStateRepository
    {
        protected readonly LoadBalancerDbContext _dbContext;
        public StateRepository(LoadBalancerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<State> GetByIdAsync(int id)
        {
            return await _dbContext.States.FindAsync(id);
        }
    }
}
