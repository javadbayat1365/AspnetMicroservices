using AutoMapper;
using Ordering.Application.Features.Commands.CheckoutOrder;
using Ordering.Application.Features.Queries.GetOrdersList;
using Ordering.Domian.Entities;

namespace Ordering.Application.Mappings;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Order,OrderDto>().ReverseMap();
        CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
    }
}
