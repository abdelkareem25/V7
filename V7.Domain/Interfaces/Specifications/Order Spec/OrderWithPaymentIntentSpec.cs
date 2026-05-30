using V7.Domain.Entites.OrderAggregate;

namespace V7.Domain.Interfaces.Specifications.Order_Spec
{
    public class OrderWithPaymentIntentSpec : BaseSpecifications<Order>
    {
        public OrderWithPaymentIntentSpec(string paymentIntentId ) 
            :base(o=>o.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
