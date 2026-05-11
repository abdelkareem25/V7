namespace V7.Domain.Entites.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrder product, int quantity, decimal price)
        {
            Product = product;
            Quantity = quantity;
            Price = price;
        }

        public ProductItemOrder Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
