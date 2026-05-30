using System.ComponentModel.DataAnnotations;

namespace V7.Api.DTOs.Order
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
