using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [HttpGet("{ProductName}",Name = "[action]")]
        [ProducesResponseType(typeof(Coupon),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Coupon),(int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Coupon>> GetDiscount(string ProductName)
        {
            var discount = await _discountRepository.GetDiscount(ProductName);
            return Ok(discount);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(bool),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CreateDiscount([FromBody] Coupon coupon)
        {
            var result = await _discountRepository.CreateDiscount(coupon);
            return CreatedAtRoute("GetDiscount", new { ProductName = coupon.ProductName },coupon);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateDiscount([FromBody] Coupon coupon)
        => Ok(await _discountRepository.UpdateDiscount(coupon));
        

        [HttpDelete("{ProductName}", Name = "[action]")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteDiscount(string ProductName)
        => Ok(await _discountRepository.DeleteDiscount(ProductName));
    }
}
