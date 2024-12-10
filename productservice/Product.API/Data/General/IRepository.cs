using Marten.Linq.Includes;
using Marten.Linq.Parsing;
using ProductCategory.API.Models;
using System.Linq.Expressions;

namespace ProductCategory.API.Data.General;

public interface IRepository<T> where T : IBaseModel
{
    Task<IEnumerable<T>> GetAllAsync<TInclude>(Expression<Func<T, bool>>? filter = null, bool isPaged = false, int ? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task<T> GetAsync(Guid Id, CancellationToken cancellationToken = default);
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task RemoveAsync(Guid Id, CancellationToken cancellationToken = default);

}
