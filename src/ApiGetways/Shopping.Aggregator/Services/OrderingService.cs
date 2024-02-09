using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class OrderingService : IOrderingService
    {
        public Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
