using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistances;
using Ordering.Application.Exceptions;
using Ordering.Domian.Entities;

namespace Ordering.Application.Features.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(
        IOrderRepository orderRepository, 
        IMapper mapper, 
        ILogger<UpdateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken token= default)
    {
        var orderEntity = await _orderRepository.GetByIdAsync(request.Id,token);

        if (orderEntity == null)
        {
            throw new NotFoundException($"The order {request.Id} not found!");
        }
        _mapper.Map(request,orderEntity,typeof(UpdateOrderCommand),typeof(Order));

        await _orderRepository.UpdateAsync(orderEntity,token);

        _logger.LogInformation($"order with Id {request.Id} Updated!");
    }
}
