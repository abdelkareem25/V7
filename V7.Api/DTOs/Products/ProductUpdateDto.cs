using System.ComponentModel.DataAnnotations;

namespace V7.Api.DTOs.Products
{
    public class ProductUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        
        public string PictureUrl { get; set; } = string.Empty;
        
        [Required]
        public int CategoryId { get; set; }
    }
}
