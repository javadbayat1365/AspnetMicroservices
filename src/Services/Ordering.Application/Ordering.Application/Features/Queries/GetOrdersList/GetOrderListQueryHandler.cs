using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistances;

namespace Ordering.Application.Features.Queries.GetOrdersList;

public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderListQueryHandler(IMapper mapper,IOrderRepository orderRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<List<OrderDto>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var orderlist = await _orderRepository.GetByUserName(request.UserName);
        return _mapper.Map<List<OrderDto>>(orderlist);
    }
}
