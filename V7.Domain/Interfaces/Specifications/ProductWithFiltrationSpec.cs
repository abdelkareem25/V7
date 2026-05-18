using V7.Domain.Entites;

namespace V7.Domain.Interfaces.Specifications
{
    public class ProductWithFiltrationSpec : BaseSpecifications<Product>
    {
        public ProductWithFiltrationSpec(ProductSpecParams param) :base(
            p =>
            (string.IsNullOrEmpty(param.Search) || p.Name.ToLower().Contains(param.Search))
            &&
            (!param.categoryId.HasValue || p.CategoryId == param.categoryId)
        )
        {
        
        }
    }   
}