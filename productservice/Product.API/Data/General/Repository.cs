using Marten;
using System.Linq.Expressions;

namespace ProductCategory.API.Data.General;

public class Repository<T> : IRepository<T> where T : class
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

    public async Task<IEnumerable<T>> GetAllAsync<TInclude>(Expression<Func<T, object>>? includeExpression = null, List<TInclude>? includeList = null, CancellationToken cancellationToken = default)
    {
        var query = _session.Query<T>();

        if (includeExpression != null && includeList != null)
        {
            query = query.Include(includeExpression, includeList);    
        }
  
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<T> GetAsync<TInclude>(Expression<Func<T, bool>> filter, Expression<Func<T, object>>? includeExpression = null, List<TInclude>? includeList = null, CancellationToken cancellationToken = default)
    {
        var query = _session.Query<T>().Where(filter);

        if (includeExpression != null && includeList != null)
        {
            query = query.Include(includeExpression, includeList);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task RemoveAsync(T entity, CancellationToken cancellationToken = default)
    {
       _session.Delete<T>(entity);
        await _session.SaveChangesAsync(cancellationToken);
    }
}
