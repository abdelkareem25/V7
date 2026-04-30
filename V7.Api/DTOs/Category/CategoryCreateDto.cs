using System.ComponentModel.DataAnnotations;

namespace V7.Api.DTOs.Category
{
    public class CategoryCreateDto
    {
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
