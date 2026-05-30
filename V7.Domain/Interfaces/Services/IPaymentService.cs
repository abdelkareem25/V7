using V7.Domain.Entites.Cart;

namespace V7.Domain.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
    }
}
