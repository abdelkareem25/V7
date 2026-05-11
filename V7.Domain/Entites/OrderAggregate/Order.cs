namespace V7.Domain.Entites.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, DelivaryMethod delivaryMethod, ICollection<OrderItem> order, decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DelivaryMethod = delivaryMethod;
            this.order = order;
            Subtotal = subtotal;
        }

        public string BuyerEmail { get; private set; }
        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; private set; }
        public DelivaryMethod DelivaryMethod { get; private set; }
        public ICollection<OrderItem> order { get; private set; } = new HashSet<OrderItem>();
        public decimal Subtotal { get; private set; }
        public decimal GetTotal()
        => Subtotal + DelivaryMethod.Cost;
        
        public string PaymentIntentId { get; private set; }
    }
}