using System.ComponentModel.DataAnnotations;

namespace V7.Api.DTOs.Order
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue , ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }
}