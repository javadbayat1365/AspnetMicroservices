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

    public async Task<T> AddAsync(T entity)
    {
        try
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

        }
        catch (Exception e)
        {

            throw;
        }
        return entity;
    }

    public async Task DeleteAsync(T entity, CancellationToken token)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(token);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken token)
    {
        return await _context.Set<T>().ToListAsync(token);
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
    {
     return  await _context.Set<T>().Where(predicate).ToListAsync(token);
    }

    public async Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeString = null, 
        bool disableTracking = true, 
        CancellationToken token = default)
    {
        IQueryable<T> query = _context.Set<T>();

        if (!disableTracking)
           query= query.AsNoTracking();

        if(string.IsNullOrWhiteSpace(includeString))
            query = query.Include(includeString);

        if (predicate != null)
            query = query.Where(predicate);

        if (orderBy != null)
           return await orderBy(query).ToListAsync(token);

        return await query.ToListAsync(token);
    }

    public async Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        List<Expression<Func<T, object>>> includes = null,
        bool disableTracking = true, 
        CancellationToken token = default)
    {
        IQueryable<T> query = _context.Set<T>();

        if (!disableTracking)
            query = query.AsNoTracking();

        if (includes != null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate != null)
            query = query.Where(predicate);

        if(orderBy != null)
           return await orderBy(query).ToListAsync(token);

        return await query.ToListAsync(token);
    }

    public async Task<T> GetByIdAsync(int Id, CancellationToken token)
    {
        return await _context.Set<T>().FindAsync(Id,token);
    }

    public async Task UpdateAsync(T entity, CancellationToken token)
    {
       _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync(token);
    }
}
