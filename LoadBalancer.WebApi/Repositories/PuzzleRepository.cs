using LoadBalancer.IdenityServer.Data.Repositories.Interfaces;
using LoadBalancer.WebApi.Data;
using LoadBalancer.WebApi.Data.Entities.PuzzleEntity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LoadBalancer.IdenityServer.Data.Repositories
{
    public class PuzzleRepository : IPuzzleRepository
    {
        protected readonly LoadBalancerDbContext _dbContext;
        public PuzzleRepository(LoadBalancerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Puzzle> AddPuzzleAsync(Puzzle puzzle)
        {
            await _dbContext.Puzzles!.AddAsync(puzzle);
            await _dbContext.SaveChangesAsync();
            return puzzle;
        }

        public Puzzle GetPuzzleById(int id)
        {
            return _dbContext.Puzzles.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Puzzle> GetPuzzlesByUser(string userId)
        {
            return _dbContext.Puzzles.Where(x => x.UserId == userId).Include(x => x.State);
        }

        public async Task<Puzzle> UpdatePuzzleAsync(Puzzle puzzle)
        {
            await Task.Run(() => _dbContext.Entry(puzzle).State = EntityState.Modified);
            await _dbContext.SaveChangesAsync();
            return puzzle;
        }
    }
}
