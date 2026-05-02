using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using V7.Domain.Entites.Cart;
using V7.Domain.Interfaces.Repositories;

namespace V7.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var Basket = await _database.StringGetAsync(basketId);
            return Basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket); //recreate the basket object from the json string JSON/XML ➜ Object
        }
       
        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var jsonBasket = JsonSerializer.Serialize(basket); //convert the basket object to a json string Object ➜ JSON/XML
            var createdOrUpdated =  await _database.StringSetAsync(basket.Id,jsonBasket, TimeSpan.FromDays(2));
            if (!createdOrUpdated) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
