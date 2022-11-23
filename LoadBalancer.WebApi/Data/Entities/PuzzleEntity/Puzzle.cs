using LoadBalancer.WebApi.Data.Entities.StateEntity;
using LoadBalancer.WebApi.Data.Entities.UzerToPuzzleEntity;

namespace LoadBalancer.WebApi.Data.Entities.PuzzleEntity
{
    public class Puzzle
    {
        public int Id { get; set; }
        public int Dimension { get; set; }
        public string? TimeResult { get; set; }
        public State? State { get; set; }  
        public int? StateId { get; set; }
        public string? UserId { get; set; }
        public UzerToPuzzle? UserToPuzzle { get; set; }
    }
}
