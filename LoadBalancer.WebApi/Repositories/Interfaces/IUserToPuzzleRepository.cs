using LoadBalancer.WebApi.Data.Entities.UzerToPuzzleEntity;

namespace LoadBalancer.WebApi.Repositories.Interfaces
{
    public interface IUserToPuzzleRepository
    {
        UserToPuzzle GetUserById(string userId);
        Task<UserToPuzzle> UpdatePuzzleAsync(UserToPuzzle user);
        Task<UserToPuzzle> AddUserToPuzzleAsync(UserToPuzzle user);
    }
}
