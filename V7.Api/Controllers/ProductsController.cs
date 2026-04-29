using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using V7.Api.DTOs.Products;
using V7.Domain.Entites;
using V7.Domain.Interfaces.Repositories;
using V7.Domain.Interfaces.Specifications;

namespace V7.Api.Controllers
{
    public class ProductsController : ApiBaseController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts()
        {
            var spec = new ProductSpecifications();
            var products = await _productRepository.GetAllAsync(spec);
            var productsDto = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            return Ok(productsDto);
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await _productRepository.GetByIdAsync(spec);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = _mapper.Map<Product, ProductDto>(product);
            return Ok(productDto);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<ProductCreateDto, Product>(productCreateDto);
            
            await _productRepository.AddAsync(product);
            
            // Reload product to get category details if needed, but for now just return
            var productToReturn = _mapper.Map<Product, ProductDto>(product);
            
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, productToReturn);
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            var spec = new ProductSpecifications(id);
            var product = await _productRepository.GetByIdAsync(spec);
            
            if (product == null)
            {
                return NotFound();
            }

            _mapper.Map(productUpdateDto, product);
            await _productRepository.UpdateAsync(product);

            return NoContent();
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await _productRepository.GetByIdAsync(spec);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteAsync(product);

            return NoContent();
        }
    }
}
