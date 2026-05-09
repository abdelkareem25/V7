using AutoMapper;
using V7.Api.DTOs.Category;
using V7.Api.DTOs.Identity;
using V7.Api.DTOs.Order;
using V7.Domain.Entites;
using V7.Domain.Entites.Cart;
using V7.Domain.Entites.Identity;

namespace V7.Api.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
        }
    }
}
