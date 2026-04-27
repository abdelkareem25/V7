
namespace V7.Domain.Entites
{
    public class Category :BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Product> Product { get; set; } = new List<Product>();
    }
}
