using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using V7.Domain.Entites.OrderAggregate;

namespace V7.Infrastructure.Data.Configurations
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(Oi=> Oi.Price)
                .HasColumnType("decimal(18,2)");

            builder.OwnsOne(Oi=>Oi.Product , x=>x.WithOwner());

        }
    }
}
