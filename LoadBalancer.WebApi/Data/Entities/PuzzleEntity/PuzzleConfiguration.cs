using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoadBalancer.WebApi.Data.Entities.PuzzleEntity
{
    public class PuzzleConfiguration : IEntityTypeConfiguration<Puzzle>
    {
        public void Configure(EntityTypeBuilder<Puzzle> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
              .Property(x => x.Dimension)
              .HasMaxLength(20)
              .IsRequired();

            builder
              .Property(x => x.TimeResult)
              .HasMaxLength(20);

            builder
                .HasOne(x => x.State)
                .WithMany(x => x.Puzzles)
                .HasForeignKey(x => x.StateId);

            builder
                .HasOne(x => x.UserToPuzzle)
                .WithMany(x => x.Puzzles)
                .HasForeignKey(x => x.UserId);
        }
    }
}
