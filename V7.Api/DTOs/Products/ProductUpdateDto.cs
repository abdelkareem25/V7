using System.ComponentModel.DataAnnotations;

namespace V7.Api.DTOs.Products
{
    public class ProductUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        
        public string PictureUrl { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
    }
}
