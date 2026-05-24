using AutoMapper;
using V7.Api.DTOs.Category;
using V7.Api.DTOs.Identity;
using V7.Api.DTOs.Order;
using V7.Domain.Entites;
using V7.Domain.Entites.Cart;
using V7.Domain.Entites.Identity;
using V7.Domain.Entites.OrderAggregate;
using IdentityAddress = V7.Domain.Entites.Identity.Address;
using AggregateAddress = V7.Domain.Entites.OrderAggregate.Address;
using V7.Api.Helper;

namespace V7.Api.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            CreateMap<IdentityAddress, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, AggregateAddress>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.DeliveryMethod, o => o.MapFrom(src => src.DeliveryMethod))
                .ForMember(dest => dest.DeliveryMethodCost, o => o.MapFrom(src => src.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, o => o.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, o => o.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.ProductUrl, o => o.MapFrom(src => src.Product.ProductUrl))
                .ForMember(dest => dest.ProductUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());
        }
    }
}
