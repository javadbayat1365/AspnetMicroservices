using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistances;
using Ordering.Domian.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(OrderContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetByUserName(string userName)
    {
        return await _context.Orders.Where(f => f.UserName == userName).ToListAsync();
    }
}
