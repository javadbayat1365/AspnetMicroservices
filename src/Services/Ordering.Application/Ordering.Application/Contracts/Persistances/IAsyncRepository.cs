using Ordering.Domian.Common;
using System.Linq.Expressions;

namespace Ordering.Application.Contracts.Persistances;

public interface IAsyncRepository<T> where T : EntityBase
{
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken token);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate,
                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                     string includeString = null,
                     bool disableTracking = true,
                     CancellationToken token = default);

    Task<T> GetAsync(Expression<Func<T, bool>> predicate,
                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                     List<Expression<Func<T, object>>> includes = null,
                     bool disableTracking = true,
                     CancellationToken token = default);
    Task<T> GetByIdAsync(int Id,CancellationToken token);
    Task<T> AddAsync(T entity, CancellationToken token);
    Task UpdateAsync(T entity, CancellationToken token);
    Task DeleteAsync(T entity, CancellationToken token);
}
