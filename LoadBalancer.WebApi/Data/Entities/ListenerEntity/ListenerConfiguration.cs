using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoadBalancer.WebApi.Data.Entities.ListenerEntity
{
    public class ListenerConfiguration : IEntityTypeConfiguration<Listener>
    {
        public void Configure(EntityTypeBuilder<Listener> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder
              .Property(x => x.Port)
              .HasMaxLength(20)
              .IsRequired();

            builder
              .Property(x => x.Load)
              .HasMaxLength(20)
              .IsRequired();
        }
    }
}
