using LoadBalancer.WebApi.Data.Entities.StateEntity;

namespace LoadBalancer.WebApi.DTOs
{
    public class PuzzleDto
    {
        public int Id { get; set; }
        public int Dimension { get; set; }
        public string? TimeResult { get; set; }
        public StateDto? State { get; set; }
    }
}
