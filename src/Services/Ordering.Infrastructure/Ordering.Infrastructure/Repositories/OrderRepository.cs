using Ordering.Application.Contracts.Persistances;
using Ordering.Domian.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(OrderContext context) : base(context)
    {
    }

    public Task<IEnumerable<Order>> GetByUserName(string userName)
    {
        throw new NotImplementedException();
    }
}
