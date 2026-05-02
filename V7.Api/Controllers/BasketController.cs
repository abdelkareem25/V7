using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V7.Domain.Entites.Cart;
using V7.Domain.Interfaces.Repositories;

namespace V7.Api.Controllers
{
    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        // GET:Recreate
        [HttpGet("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            return basket is null ? new CustomerBasket(basketId) : Ok(basket);
        }

        // Update or Create
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var updatedOrCreateBasket = await _basketRepository.UpdateBasketAsync(basket);
            if(updatedOrCreateBasket is null)
            {
                return BadRequest("Problem updating the basket");
            }
            return Ok(updatedOrCreateBasket);
        }

        // Delete
        [HttpDelete("{basketId}")]
        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
            return await _basketRepository.DeleteBasketAsync(basketId);
        }
    }
}
