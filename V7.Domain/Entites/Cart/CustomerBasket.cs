using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V7.Domain.Entites.Cart
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }

        public CustomerBasket(string id)
        {
            Id = id;
            
        }
    }
}
