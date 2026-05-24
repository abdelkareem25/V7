using V7.Domain.Entites.OrderAggregate;

namespace V7.Api.DTOs.Order
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}