
namespace V7.Domain.Entites
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = string.Empty;

        public int CategoryId { get; set; } // FK
        public Category? Category { get; set; }
    }
}
