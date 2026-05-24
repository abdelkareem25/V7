using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using V7.Domain.Entites.OrderAggregate;

namespace V7.Infrastructure.Data.Configurations
{
    public class DeliveryMethodConfig : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(Dm => Dm.Cost)
                .HasColumnType("decimal(18,2)");
        }
    }
}
