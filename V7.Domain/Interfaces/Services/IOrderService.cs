using V7.Domain.Entites.OrderAggregate;

namespace V7.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string BuyerEmail,string BasketId,int DeleviryMethodId , Address ShippingAddress);
        Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(String BuyerEmail);
        Task<Order> GetOrderByIdForSpecificUserAsync(string BuyerEmail,int OrderId);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
