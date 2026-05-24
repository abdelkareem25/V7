namespace V7.Domain.Entites.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod delivaryMethod, ICollection<OrderItem> items, decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = delivaryMethod;
            Items = items;
            Subtotal = subtotal;
        }

        public string BuyerEmail { get; private set; }
        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; private set; }
        public DeliveryMethod DeliveryMethod { get; private set; }
        public ICollection<OrderItem> Items { get; private set; } = new HashSet<OrderItem>();
        public decimal Subtotal { get; private set; }
        public decimal GetTotal()
        => Subtotal + DeliveryMethod.Cost;
        
        public string PaymentIntentId { get; private set; }
    }
}