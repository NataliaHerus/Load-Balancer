using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoadBalancer.WebApi.Data.Entities.StateEntity
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
             .Property(x => x.Name)
             .HasMaxLength(20)
             .IsRequired();
        }
    }
}
