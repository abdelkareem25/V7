using System.ComponentModel.DataAnnotations;

namespace V7.Api.DTOs.Category
{
    public class CategoryDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
