using AutoMapper;
using V7.Api.DTOs.Category;
using V7.Domain.Entites;

namespace V7.Api.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
