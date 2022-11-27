using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoadBalancer.WebApi.Data.Entities.UzerToPuzzleEntity
{
    public class UserToPuzzleConfuguration : IEntityTypeConfiguration<UserToPuzzle>
    {
        public void Configure(EntityTypeBuilder<UserToPuzzle> builder)
        {
            builder
             .Property(x => x.UserId);

            builder.HasKey(x => x.UserId);

            builder
                .Property(x => x.MaxCount)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
