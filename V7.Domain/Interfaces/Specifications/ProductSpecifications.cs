using V7.Domain.Entites;

namespace V7.Domain.Interfaces.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product>
    {
        // all products with category
        public ProductSpecifications(ProductSpecParams param) :
            base(p =>
            (string.IsNullOrEmpty(param.Search)||p.Name.ToLower().Contains(param.Search))
            &&
            (!param.categoryId.HasValue || p.CategoryId == param.categoryId) 
            )
        {
            Includes.Add(p => p.Category);
            if (!string.IsNullOrEmpty(param.sort))
            {
                switch (param.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

            ApplyPagination(param.PageSize*(param.PageIndex-1), param.PageSize);
        }
        // byID
        public ProductSpecifications(int id) :base(p => p.Id == id)
        {
            Includes.Add(p => p.Category);
        }


    }
}
