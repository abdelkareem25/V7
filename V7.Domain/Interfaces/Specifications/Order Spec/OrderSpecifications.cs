using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V7.Domain.Entites.OrderAggregate;

namespace V7.Domain.Interfaces.Specifications.Order_spec
{
    public class OrderSpecifications : BaseSpecifications<Order>
    {
        public OrderSpecifications(string email):base(o=>o.BuyerEmail == email)
        {
            Includes.Add(o=>o.DeliveryMethod);
            Includes.Add(o => o.Items);
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrderSpecifications(int orderId, string email) : base(o=>o.BuyerEmail == email && o.Id == orderId)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
        }
    }
}
