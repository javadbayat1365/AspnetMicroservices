using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructures;
using Ordering.Application.Contracts.Persistances;
using Ordering.Domian.Entities;

namespace Ordering.Application.Features.Commands.CheckoutOrder;

internal class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;

    public CheckoutOrderCommandHandler(
        IOrderRepository orderRepository, 
        IEmailService emailService, 
        IMapper mapper, 
        ILogger<CheckoutOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _emailService = emailService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = await _orderRepository.AddAsync(_mapper.Map<Order>(request));
        _logger.LogInformation($"the order with ID : {orderId} added");
         await SendEmailAsync(orderId.Id);
        return orderId.Id;
    }

    private async Task SendEmailAsync(int orderId)
    {
        try
        {
           await _emailService.SendEmailAsync(new Models.Email(
                  "javadbayat.1365@gmail.com",
                  "Send Order!",
                  $"The order with {orderId} registered and is in proccess..."
                ));
        }
        catch (Exception)
        {
            _logger.LogError("An error accured!");
        }
    }
}
