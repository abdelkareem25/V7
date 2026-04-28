
namespace V7.Domain.Entites
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } 

        public int CategoryId { get; set; } // FK
        public Category? Category { get; set; }
    }
}
