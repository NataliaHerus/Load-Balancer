using LoadBalancer.WebApi.Data.Entities.StateEntity;

namespace LoadBalancer.WebApi.Repositories.Interfaces
{
    public interface IStateRepository
    {
        public Task<State> GetByIdAsync(int id);
    }
}
