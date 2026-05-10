namespace V7.Domain.Entites.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
       public ProductItemOrder Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
