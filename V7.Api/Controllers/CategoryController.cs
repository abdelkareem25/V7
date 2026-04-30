using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V7.Api.DTOs.Category;
using V7.Domain.Entites;
using V7.Domain.Interfaces.Repositories;
using V7.Domain.Interfaces.Specifications;

namespace V7.Api.Controllers
{
    public class CategoryController : ApiBaseController
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
        {
            var spec = new CategoryWithProductsSpecification();
            var categories = await _categoryRepository.GetAllAsync(spec);
            var categoryDtos = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryDto>>(categories);
            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var spec = new CategoryWithProductsSpecification(id);
            var category = await _categoryRepository.GetByIdAsync(spec);
            if (category == null) return NotFound();
            var categoryDto = _mapper.Map<Category, CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            var spec = new CategoryWithProductsSpecification(id);
            var category = await _categoryRepository.GetByIdAsync(spec);
            if (category == null) return NotFound();
            _mapper.Map(categoryDto, category);
            await _categoryRepository.UpdateAsync(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var spec = new CategoryWithProductsSpecification(id);
            var category = await _categoryRepository.GetByIdAsync(spec);
            if (category == null) return NotFound();
            await _categoryRepository.DeleteAsync(category);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryDto categoryDto)
        { 
            var category = _mapper.Map<CategoryDto, Category>(categoryDto);
            await _categoryRepository.AddAsync(category);
            var categoryToReturn = _mapper.Map<Category, CategoryDto>(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, categoryToReturn);
        }
    }
}
