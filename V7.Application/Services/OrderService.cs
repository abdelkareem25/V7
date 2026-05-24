using V7.Domain.Entites;
using V7.Domain.Entites.OrderAggregate;
using V7.Domain.Interfaces;
using V7.Domain.Interfaces.Repositories;
using V7.Domain.Interfaces.Services;
using V7.Domain.Interfaces.Specifications.Order_spec;

namespace V7.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository
            , IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeleviryMethodId, Address ShippingAddress)
        {
            // Retrieve the basket using the provided BasketId
            var Basket = await _basketRepository.GetBasketAsync(BasketId);

            // Create a list to hold the order items
            var OrderItems = new List<OrderItem>();
            if (Basket?.Items.Count > 0)
            { 
                foreach(var item in Basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var ProductItemOrder = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                    var OrderItem = new OrderItem(ProductItemOrder ,item.Quantity,product.Price);
                    OrderItems.Add(OrderItem);
                }
            }
            // Calculate the subtotal by summing the price of each order item multiplied by its quantity
            var Subtotal = OrderItems.Sum(s=>s.Price * s.Quantity);
            //get delivery method from delivery method repository
            var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeleviryMethodId);

            // Create a new order using the retrieved information
            var Order = new Order(BuyerEmail, ShippingAddress, DeliveryMethod, OrderItems, Subtotal);
            // add the order to the database using the order repository
             await _unitOfWork.Repository<Order>().AddAsync(Order);

            // save the changes to the database
            var Result = await _unitOfWork.CompleteAsync();
            if(Result <= 0) return null;
            return Order;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            
            var DeliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return DeliveryMethods;
        }

        public async Task<Order> GetOrderByIdForSpecificUserAsync(string BuyerEmail, int OrderId)
        {
            var spec = new OrderSpecifications(OrderId, BuyerEmail);
            var Order = await _unitOfWork.Repository<Order>().GetByIdAsync(spec);
            return Order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail)
        {
            var spec = new OrderSpecifications(BuyerEmail);
            var Orders = await _unitOfWork.Repository<Order>().GetAllAsync(spec);
            return Orders;
        }
    }
}
