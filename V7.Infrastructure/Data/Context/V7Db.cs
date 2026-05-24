using Microsoft.EntityFrameworkCore;
using V7.Domain.Entites;
using V7.Domain.Entites.OrderAggregate;

namespace V7.Infrastructure.Data.Context
{
    public class V7Db : DbContext
    {
        public V7Db(DbContextOptions<V7Db> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(V7Db).Assembly);

            modelBuilder.Entity<Product>()
                .HasData(
                    new Product { Id = 1, Name = "Iced Coffee", Description = "a refreshing, chilled coffee beverage made by brewing hot coffee (like drip or espresso) and pouring it over ice or chilling it", Price = 10.99m, CategoryId = 1 },
                    new Product { Id = 2, Name = "Hot Tea", Description = "a soothing, warm beverage made by steeping tea leaves in hot water", Price = 19.99m, CategoryId = 1 },
                    new Product { Id = 3, Name = "Cream cheese", Description = "a rich, creamy cheese made from milk and cream", Price = 15.99m, CategoryId = 2 }
                );
            modelBuilder.Entity<Category>()
                .HasData(
                    new Category { Id = 1, Name = "Drinks" , Description = "liquids meant for human consumption, ranging from refreshing hydration to flavorful indulgence" },
                    new Category { Id = 2, Name = "Food", Description = "edible substances that provide nutritional support for the body, encompassing a wide variety of flavors and textures" }
                );
            modelBuilder.Entity<DeliveryMethod>()
                .HasData(
                    new DeliveryMethod { Id = 1, ShortName = "Standard", Description = "Standard delivery", DeliveryTime = "5-7 days", Cost = 5.00m },
                    new DeliveryMethod { Id = 2, ShortName = "Express", Description = "Express delivery", DeliveryTime = "1-2 days", Cost = 15.00m }
                );
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DelivaryMethods { get; set; }
    }
}
