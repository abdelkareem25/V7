using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using V7.Domain.Entites.OrderAggregate;

namespace V7.Infrastructure.Data.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(st=>st.Subtotal).HasColumnType("decimal(18,2)");
            builder.Property(St => St.Status)
                .HasConversion(os=>os.ToString() , os => (OrderStatus) Enum.Parse(typeof(OrderStatus), os));  // from enum to string and from string to enum

            builder.OwnsOne(Ad => Ad.ShippingAddress, x => x.WithOwner()); // Mapping the owned type ShippingAddress as a separate table with a foreign key to the Order table. The WithOwner() method specifies that the ShippingAddress is owned by the Order entity, and it will be stored in a separate table with a foreign key relationship to the Order table.

            builder.HasOne(O=>O.DelivaryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
