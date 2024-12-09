using Marten.Linq.Includes;
using System.Linq.Expressions;

namespace ProductCategory.API.Data.General;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync<TInclude>(Expression<Func<T, object>>? includeExpression = null, List<TInclude>? includeList = null, CancellationToken cancellationToken = default);
    Task<T> GetAsync<TInclude>(Expression<Func<T, bool>> filter, Expression<Func<T, object>>? includeExpression = null, List<TInclude>? includeList = null, CancellationToken cancellationToken = default);
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task RemoveAsync(T entity, CancellationToken cancellationToken = default);

}
