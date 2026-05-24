using V7.Domain.Entites.OrderAggregate;

namespace V7.Api.DTOs.Order
{
    public class OrderToReturnDto
    {
        public string BuyerEmail { get; private set; }
        public DateTimeOffset OrderDate { get; private set; }
        public string Status { get; private set; }
        public Address ShippingAddress { get; private set; }
        public string DeliveryMethod { get; private set; }
        public decimal DeliveryMethodCost { get; private set; }
        public ICollection<OrderItemDto> Items { get; private set; } = new HashSet<OrderItemDto>();
        public decimal Subtotal { get; private set; }
        public decimal Total { get; private set; }
        

        public string PaymentIntentId { get; private set; }
    }
}
