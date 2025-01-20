using System.Linq.Expressions;

namespace Main.ProductService.ProductCategory.API.InitialData;
public interface IRepository<T> where T : IBaseModel
{
    Task<IEnumerable<T>> GetAllAsync<TInclude>(Expression<Func<T, bool>>? filter = null, bool isPaged = false, int? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task<T> GetAsync(Guid Id, CancellationToken cancellationToken = default);
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task RemoveAsync(Guid Id, CancellationToken cancellationToken = default);

}
