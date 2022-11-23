using LoadBalancer.WebApi.Data.Entities.PuzzleEntity;

namespace LoadBalancer.WebApi.Data.Entities.StateEntity
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Puzzle>? Puzzles { get; set; }
    }
}
