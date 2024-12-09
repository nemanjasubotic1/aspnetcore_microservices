using Marten;
using Marten.Pagination;
using ProductCategory.API.Models;

namespace ProductCategory.API.Data.General;

public class Repository<T> : IRepository<T> where T : IBaseModel
{
    private readonly IDocumentSession _session;
    public Repository(IDocumentSession session)
    {
        _session = session;
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _session.Store<T>(entity);
        await _session.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync<TInclude>(int? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default) 
    {
        var listOfT = _session.Query<T>();

        return await listOfT.ToPagedListAsync(pageNumber ?? 1, pageSize ?? 1, cancellationToken);
    }

    public async Task<T> GetAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        var query = await _session.LoadAsync<T>(Id, cancellationToken);
      
        if (query == null)
        {
            return default(T);
        }

        return query;
    }

    public async Task RemoveAsync(Guid Id, CancellationToken cancellationToken = default)
    {
       _session.Delete<T>(Id);
        await _session.SaveChangesAsync(cancellationToken);
    }
}
