using AutoMapper;
using Basket.API.Entities;
using Basket.API.Entities.Repositories;
using Basket.API.GrpcServices;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountGrpcService _discountGrpcService;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        this._basketRepository = basketRepository;
        _discountGrpcService = discountGrpcService;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet("{userName}",Name ="GetBasket")]
    [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
    {
       var basket = await _basketRepository.GetBasket(userName);
        return Ok(basket ?? new ShoppingCart(userName));
    }

    [HttpPost(Name = "UpdateBasket")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody]ShoppingCart shoppingCart)
    {
        //TODO: Get discount from Grpc service of discount.Grpc service
        //foreach (var item in shoppingCart.shoppingCartItems)
        //{
        //    var coupon =await _discountGrpcService.GetDiscount(item.ProductName);
        //    item.Price -= coupon.Amount;
        //}

        var basket = await _basketRepository.UpdateBasket(shoppingCart);
        return Ok(basket);
    }

    [HttpDelete("{userName}",Name = "DeleteBasket")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> DeleteBasket(string userName)
    {
        await _basketRepository.DeleteBasket(userName);
        return Ok();
    }


    [HttpPost("[action]")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout,CancellationToken token)
    {
        var basket =await _basketRepository.GetBasket(basketCheckout.UserName);
        if (basket == null)
            return BadRequest();

        var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
        eventMessage.TotalPrice = basket.TotalPrice;
       await _publishEndpoint.Publish(eventMessage,token);


       await _basketRepository.DeleteBasket(basketCheckout.UserName);

        return Accepted();
    }
}
