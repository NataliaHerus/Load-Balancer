using LoadBalancer.WebApi.Data.Entities.StateEntity;
using LoadBalancer.WebApi.Enums;
using Microsoft.EntityFrameworkCore;

namespace LoadBalancer.WebApi.Data.SeedData
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder builder)
        {
            SeedPuzzlesStates(builder);
        }

        public static void SeedPuzzlesStates(ModelBuilder builder)
        {
            builder.Entity<State>().HasData(
                new State()
                {
                    Id = (int)PuzzleState.InProgress,
                    Name = "InProgress"
                },
                new State()
                {
                    Id = (int)PuzzleState.Completed,
                    Name = "Completed"
                },
                new State()
                {
                    Id = (int)PuzzleState.Canceled,
                    Name = "Canceled"
                },
                new State()
                {
                    Id = (int)PuzzleState.Failed,
                    Name = "Failed"
                });
        }

    }
}
