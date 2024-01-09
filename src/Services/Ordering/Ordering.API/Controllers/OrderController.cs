using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Commands.CheckoutOrder;
using Ordering.Application.Features.Commands.DeleteOrder;
using Ordering.Application.Features.Commands.UpdateOrder;
using Ordering.Application.Features.Queries.GetOrdersList;
using System.Net;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userName}",Name ="[action]")]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName(string userName,CancellationToken token=default)
        => Ok(await _mediator.Send(new GetOrderListQuery(userName), token));

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(int),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand model, CancellationToken token = default)
         => Ok(await _mediator.Send(model, token));

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand model)
        {
            await _mediator.Send(model);
            return NoContent();
        }

        [HttpDelete("{Id}",Name ="[action]")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteOrder(int Id)
        {
           await _mediator.Send(new DeleteOrderCommand(Id));
            return NoContent();
        }
    }
}
