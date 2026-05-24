using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using V7.Api.DTOs.Products;
using V7.Api.Helper;
using V7.Domain.Entites;
using V7.Domain.Interfaces;
using V7.Domain.Interfaces.Specifications;

namespace V7.Api.Controllers
{
    public class ProductsController : ApiBaseController
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams param)
        {
            var spec = new ProductSpecifications(param);
            var products = await _unitOfWork.Repository<Product>().GetAllAsync(spec);
            var productsDto = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            var countSpec = new ProductWithFiltrationSpec(param);
            var count = await _unitOfWork.Repository<Product>().GetCountAsync(countSpec);

            return Ok(new Pagination<ProductDto>(param.PageIndex, param.PageSize, products.Count, productsDto)); 
        } 

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(spec);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = _mapper.Map<Product, ProductDto>(product);
            return Ok(productDto);
        }

        // POST: api/products
        [Authorize]
        [HttpPost]

        public async Task<ActionResult<ProductDto>> CreateProduct(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<ProductCreateDto, Product>(productCreateDto);
            
            await _unitOfWork.Repository<Product>().AddAsync(product);
            
            // Reload product to get category details if needed, but for now just return
            var productToReturn = _mapper.Map<Product, ProductDto>(product);
            
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, productToReturn);
        }

        // PUT: api/products/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            var spec = new ProductSpecifications(id);
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(spec);
            
            if (product == null)
            {
                return NotFound();
            }

            _mapper.Map(productUpdateDto, product);
            await _unitOfWork.Repository<Product>().UpdateAsync(product);

            return NoContent();
        }

        // DELETE: api/products/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(spec);
            if (product == null)
            {
                return NotFound();
            }

            await _unitOfWork.Repository<Product>().DeleteAsync(product);

            return NoContent();
        }
    }
}
