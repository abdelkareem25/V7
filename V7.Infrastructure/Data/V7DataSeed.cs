using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using V7.Domain.Entites;
using V7.Infrastructure.Data.Context;

namespace V7.Infrastructure.Data
{
    public static class V7DataSeed
    {
        public static async Task SeedAsync(V7Db db)
        {
            if (!db.Products.Any())
            {
                var ProductsData = File.ReadAllText("../V7.Infrastructure/Data/DataSeeding/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await db.Set<Product>().AddAsync(product);
                    }
                    await db.SaveChangesAsync();
                }
            }
            
        }
    }
}
