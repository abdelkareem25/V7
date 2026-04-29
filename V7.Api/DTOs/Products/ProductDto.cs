using System.ComponentModel.DataAnnotations;

namespace V7.Api.DTOs.Products
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } 
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]  
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}
