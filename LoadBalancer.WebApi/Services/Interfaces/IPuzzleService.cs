using LoadBalancer.WebApi.DTOs;

namespace LoadBalancer.WebApi.Services.Interfaces
{
    public interface IPuzzleService
    {
        Task<PuzzleDto> CreatePuzzleAsync(PuzzleRequestDto dto);
        IEnumerable<PuzzleDto> GetPuzzlesByUser(string userId);
        Task<PuzzleDto> CompletePuzzleAsync(int id);
        PuzzleDto GetPuzzlesById(int id);
    }
}
