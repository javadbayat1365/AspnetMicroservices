using Basket.API.Entities;
using Basket.API.Entities.Repositories;
using Basket.API.GrpcServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountGrpcService _discountGrpcService;
    public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
    {
        this._basketRepository = basketRepository;
        _discountGrpcService = discountGrpcService;
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
        foreach (var item in shoppingCart.shoppingCartItems)
        {
            var coupon =await _discountGrpcService.GetDiscount(item.ProductName);
            item.Price -= coupon.Amount;
        }

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
}
