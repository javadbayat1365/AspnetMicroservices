using Ordering.Domian.Entities;

namespace Ordering.Application.Contracts.Persistances;

public interface IOrderRepository:IAsyncRepository<Order>
{
    Task<IEnumerable<Order>> GetByUserName(string userName);
}
