using System.ComponentModel.DataAnnotations;
using V7.Api.DTOs.Identity;
using V7.Domain.Entites.OrderAggregate;

namespace V7.Api.DTOs.Order
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }

        public int DelivaryMethodId { get; set; }

        public AddressDto ShippingAddress { get; set; }
    }
}
