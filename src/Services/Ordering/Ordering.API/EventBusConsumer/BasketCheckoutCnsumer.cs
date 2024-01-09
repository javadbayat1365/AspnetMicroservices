using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Commands.CheckoutOrder;

namespace Ordering.API.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public BasketCheckoutConsumer(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var checkoutOrderCommand = _mapper.Map<CheckoutOrderCommand>(context.Message);
            var result = await _mediator.Send(checkoutOrderCommand);

            _logger.LogInformation($"Basket checkout event consumed successfully. Created orderId:{result}");
        }
    }
}
