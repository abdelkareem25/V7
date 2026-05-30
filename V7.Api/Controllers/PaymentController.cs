using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V7.Api.DTOs.Order;
using V7.Domain.Interfaces.Services;
using Stripe;
namespace V7.Api.Controllers
{
    [Authorize]
    public class PaymentController : ApiBaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        const string endpointSecret = "whsec";
        public PaymentController(IPaymentService paymentService
            , IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [ProducesResponseType((typeof(CustomerBasketDto)), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var CustomerBasket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (CustomerBasket == null) return BadRequest(new ProblemDetails { Title = "Problem with your basket" });
            var MappedCustomerBasket = _mapper.Map<CustomerBasketDto>(CustomerBasket);
            return Ok(MappedCustomerBasket);

        }

        //[HttpPost("webhook")]
        //public async Task<ActionResult> StripeWebhook()
        //{
        //    var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        //    try
        //    {
        //        var stripeEvent = Stripe.EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], endpointSecret);
        //        var paymentIntent = stripeEvent.Data.Object as Stripe.PaymentIntent;
        //        if (stripeEvent.Type == Events.PaymentIntentSucceeded)
        //        {

        //        }
        //        else if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
        //        {

        //        }
        //        return Ok();
        //    }
        //    catch (StripeException ex)
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
