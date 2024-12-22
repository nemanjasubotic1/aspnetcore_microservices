using Marten;
using Marten.Linq;
using Marten.Pagination;
using System.Linq.Expressions;

namespace Main.ProductService.ProductCategory.API.InitialData;

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

    public async Task<IEnumerable<T>> GetAllAsync<TInclude>(Expression<Func<T, bool>>? filter = null, bool isPaged = false, int? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var listOfT = _session.Query<T>();

        if (filter != null)
        {
            listOfT = (IMartenQueryable<T>)listOfT.Where(filter);
        }

        if (isPaged)
        {
            return await listOfT.ToPagedListAsync(pageNumber ?? 1, pageSize ?? 10, cancellationToken);
        }

        return await listOfT.ToListAsync();
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
