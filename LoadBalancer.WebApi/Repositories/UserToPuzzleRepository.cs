using LoadBalancer.WebApi.Data;
using LoadBalancer.WebApi.Data.Entities.UzerToPuzzleEntity;
using LoadBalancer.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoadBalancer.WebApi.Repositories
{
    public class UserToPuzzleRepository : IUserToPuzzleRepository
    {
        protected readonly LoadBalancerDbContext _dbContext;
        public UserToPuzzleRepository(LoadBalancerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserToPuzzle> AddUserToPuzzleAsync(UserToPuzzle user)
        {
            await _dbContext.UserToPuzzle.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public UserToPuzzle GetUserById(string userId)
        {
            return _dbContext.UserToPuzzle.FirstOrDefault(x => x.UserId == userId);
        }

        public async Task<UserToPuzzle> UpdatePuzzleAsync(UserToPuzzle user)
        {
            await Task.Run(() => _dbContext.Entry(user).State = EntityState.Modified);
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
