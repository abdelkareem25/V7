using AutoMapper;
using V7.Api.DTOs.Products;
using V7.Domain.Entites;

namespace V7.Api.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category != null ? s.Category.Name : string.Empty));
                
            CreateMap<ProductCreateDto, Product>();
            
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
