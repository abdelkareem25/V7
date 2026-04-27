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
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
