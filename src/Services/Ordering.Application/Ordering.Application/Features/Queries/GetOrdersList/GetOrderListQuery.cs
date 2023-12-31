using MediatR;

namespace Ordering.Application.Features.Queries.GetOrdersList;

public record GetOrderListQuery(string UserName): IRequest<IEnumerable<OrderDto>>;
