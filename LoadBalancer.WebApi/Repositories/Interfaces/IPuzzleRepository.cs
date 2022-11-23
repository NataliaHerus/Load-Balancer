using LoadBalancer.WebApi.Data.Entities.PuzzleEntity;

namespace LoadBalancer.IdenityServer.Data.Repositories.Interfaces
{
    public interface IPuzzleRepository
    {
        Task<Puzzle> AddPuzzleAsync(Puzzle puzzle);
        Task<Puzzle> UpdatePuzzleAsync(Puzzle puzzle);
        Puzzle GetPuzzleById(int id);
        IEnumerable<Puzzle> GetPuzzlesByUser(string userId);
    }
}
