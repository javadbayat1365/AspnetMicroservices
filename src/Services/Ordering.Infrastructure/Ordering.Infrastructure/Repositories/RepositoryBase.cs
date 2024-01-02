using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistances;
using Ordering.Domian.Common;
using Ordering.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ordering.Infrastructure.Repositories;

public class RepositoryBase<T>:IAsyncRepository<T> where T : EntityBase
{
    protected readonly OrderContext _context;
    public RepositoryBase(OrderContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity, CancellationToken token)
    {
        await _context.Set<T>().AddAsync(entity, token);
        await _context.SaveChangesAsync(token);
        return entity;
    }

    public Task DeleteAsync(T entity, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken token)
    {
        return await _context.Set<T>().ToListAsync(token);
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
    {
     return  await _context.Set<T>().Where(predicate).ToListAsync(token);
    }

    public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIdAsync(int Id, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(T entity, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
