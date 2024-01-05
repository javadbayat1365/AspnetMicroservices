using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domian.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext dbContext,ILogger<OrderContextSeed> logger,CancellationToken token)
        {
            if (!dbContext.Orders.Any())
            {
              await  dbContext.Orders.AddRangeAsync(SeedOrders(), token);
            }
        }

        private static IEnumerable<Order> SeedOrders()
        {
            return  new List<Order>() { 
                new Order() {
                 UserName = "ariaariai",LastName="Javad",FirstName="Bayat",EmailAddress="ariaariai@yahoo.com", CardName="50222910"
                }
            };
        }
    }
}
