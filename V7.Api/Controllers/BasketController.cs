using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using V7.Api.DTOs.Order;
using V7.Domain.Entites.Cart;
using V7.Domain.Interfaces.Repositories;

namespace V7.Api.Controllers
{
    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        // GET:Recreate
        [HttpGet("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            return basket is null ? new CustomerBasket(basketId) : Ok(basket);
        }

        // Update or Create
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var customerBasket = _mapper.Map<CustomerBasketDto , CustomerBasket>(basket);
            var updatedOrCreateBasket = await _basketRepository.UpdateBasketAsync(customerBasket);
            if (updatedOrCreateBasket is null)
            {
                return BadRequest("Problem updating the basket");
            }
            return Ok(updatedOrCreateBasket);
        }

        // Delete
        [Authorize]
        [HttpDelete("{basketId}")]
        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
            return await _basketRepository.DeleteBasketAsync(basketId);
        }
    }
} 
