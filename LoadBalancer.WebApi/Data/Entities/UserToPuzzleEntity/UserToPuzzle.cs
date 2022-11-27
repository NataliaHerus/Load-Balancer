using LoadBalancer.WebApi.Data.Entities.PuzzleEntity;

namespace LoadBalancer.WebApi.Data.Entities.UzerToPuzzleEntity
{
    public class UserToPuzzle
    {
        public string UserId { get; set; }
        public int MaxCount { get; set; }
        public ICollection<Puzzle> Puzzles { get; set; }
    }
}
