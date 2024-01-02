using Microsoft.EntityFrameworkCore;
using Ordering.Domian.Common;
using Ordering.Domian.Entities;

namespace Ordering.Infrastructure.Persistence;

public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Order>  orders { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        foreach (var item in ChangeTracker.Entries<EntityBase>())
        {
            if (item.State == EntityState.Added)
            {
                item.Entity.CreatedDate = DateTime.Now;
                item.Entity.CreatedBy = "Aria";
            }
            else if (item.State == EntityState.Modified)
            {
                item.Entity.LastModifiedDate = DateTime.Now;
                item.Entity.LastModifiedBy = "Aria";
            }
        }

        return base.SaveChangesAsync(token);
    }


}


