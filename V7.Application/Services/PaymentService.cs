using Microsoft.Extensions.Configuration;
using Stripe;
using V7.Domain.Entites.Cart;
using V7.Domain.Entites.OrderAggregate;
using V7.Domain.Interfaces;
using V7.Domain.Interfaces.Repositories;
using V7.Domain.Interfaces.Services;
using Product = V7.Domain.Entites.Product;

namespace V7.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration 
            , IBasketRepository basketRepository
            , IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            // Secret key 
            StripeConfiguration.ApiKey = _configuration["StripeKeys:SecretKey"];
            // Get the basket from the repository
            var Baskit =await _basketRepository.GetBasketAsync(basketId);
            if(Baskit == null) return null;
            // Calculate the total amount (SubTotal + DeliveryMethodId)
            var shippingPrice = 0m;
            if (Baskit.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(Baskit.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Cost;
            }
            if (Baskit.Items.Count > 0)
            {
                foreach(var item in Baskit.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    if(item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }
                }
            }
            var subtotal = Baskit.Items.Sum(x => x.Quantity * x.Price);


            // Create or update the payment intent
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(Baskit.PaymentIntentId)) // Create a new payment intent
            { var options = new PaymentIntentCreateOptions()
            {
                Amount = (long)(subtotal *100 + shippingPrice*100),
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };
                paymentIntent = await service.CreateAsync(options);
                Baskit.PaymentIntentId = paymentIntent.Id;
                Baskit.ClientSecret = paymentIntent.ClientSecret;
            }
            else // Update the existing payment intent
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(subtotal * 100 + shippingPrice * 100)
                };
                paymentIntent = await service.UpdateAsync(Baskit.PaymentIntentId, options);
                Baskit.PaymentIntentId = paymentIntent.Id;
                Baskit.ClientSecret = paymentIntent.ClientSecret;
            }
            await _basketRepository.UpdateBasketAsync(Baskit); // Update the basket with the new payment intent details
            return Baskit;
        }
    }
}
