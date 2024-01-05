using Ordering.Domian.Common;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ordering.Application.Contracts.Persistances;

public interface IAsyncRepository<T> where T : EntityBase
{
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken token);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate,
                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                     string includeString = null,
                     bool disableTracking = true,
                     CancellationToken token = default);

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate,
                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                     List<Expression<Func<T, object>>> includes = null,
                     bool disableTracking = true,
                     CancellationToken token = default);
    Task<T> GetByIdAsync(int Id,CancellationToken token);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity, CancellationToken token);
    Task DeleteAsync(T entity, CancellationToken token);
}
