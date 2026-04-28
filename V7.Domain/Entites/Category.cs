
namespace V7.Domain.Entites
{
    public class Category :BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public ICollection<Product> Product { get; set; } = new List<Product>();
    }
}
