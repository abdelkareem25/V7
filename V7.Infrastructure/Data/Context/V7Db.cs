using Microsoft.EntityFrameworkCore;
using V7.Domain.Entites;

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
                    new Product { Id = 1, Name = "Product 1", Description = "Description for Product 1", Price = 10.99m, CategoryId = 1 },
                    new Product { Id = 2, Name = "Product 2", Description = "Description for Product 2", Price = 19.99m, CategoryId = 1 },
                    new Product { Id = 3, Name = "Product 3", Description = "Description for Product 3", Price = 5.99m, CategoryId = 2 }
                );
            modelBuilder.Entity<Category>()
                .HasData(
                    new Category { Id = 1, Name = "Category 1" , Description = "Description for Category 1" },
                    new Category { Id = 2, Name = "Category 2", Description = "Description for Category 2" }
                );
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
