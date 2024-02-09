using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System.Net;

namespace Shopping.Aggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingsController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IOrderingService _orderingService;
        private readonly IBasketService _basketService;

        public ShoppingsController(
            ICatalogService catalogService,
            IOrderingService orderingService,
            IBasketService basketService)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _orderingService = orderingService;
            _basketService = basketService;
        }

        [HttpGet("{userName}", Name = "[action]")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            var basket = await _basketService.GetBasket(userName);
            foreach (var item in basket.Items)
            {
                var product = await _catalogService.GetCatalog(item.ProductId);

                item.ImageFile = product.ImageFile;
                item.Price = product.Price;
                item.Summery = product.Summary;
                item.Description = product.Description;
                item.Category = product.Category;
            }

            var order = await _orderingService.GetOrdersByUserName(userName);

            var shoppingModel = new ShoppingModel
            {
                BasketModel = basket,
                Orders = order,
                UserName = userName
            };
            return Ok(shoppingModel);
        }
    }
}
