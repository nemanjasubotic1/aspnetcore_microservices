using GeneralUsing.Extensions;
using Marten;
using Microsoft.Extensions.Caching.Distributed;

namespace Main.ProductService.ProductCategory.API.InitialData;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly IDocumentSession _session;

    private readonly IDistributedCache _cache;
    public ProductRepository(IDocumentSession session, IDistributedCache cache) : base(session)
    {
        _session = session;
        _cache = cache;
    }

    public async Task<List<Product>> GetAllProductsWithCategory(CancellationToken cancellationToken, int? pageNumber = null, int? pageSize = null)
    {
        var cachedProducts = await _cache.GetRecordAsync<IEnumerable<Product>>(DateTime.Now.ToString("yyyyMMdd_hhmm"), cancellationToken);

        if (cachedProducts != null)
        {
            return cachedProducts.ToList();
        }

        var allProducts = await GetAllAsync<Product>(filter: null, isPaged: true, pageNumber ?? 1, pageSize ?? 10, cancellationToken);

        var categoryIds = allProducts.Select(x => x.CategoryId).Distinct().ToList();

        var categories = await _session.Query<Category>().Where(l => categoryIds.Contains(l.Id)).ToListAsync(cancellationToken);

        var categoryDictionary = categories.ToDictionary(l => l.Id);

        foreach (var product in allProducts)
        {
            if (categoryDictionary.TryGetValue(product.CategoryId, out var category))
            {
                product.Category = category.Name;
            }
        }

        await _cache.SetRecordAsync<IEnumerable<Product>>(DateTime.Now.ToString("yyyyMMdd_hhmm"), allProducts, null, cancellationToken);

        return allProducts.ToList();
    }

    public async Task<Product> GetProductByIdWithCategory(Guid Id, CancellationToken cancellationToken)
    {
        var product = await GetAsync(Id, cancellationToken);

        var categories = await _session.Query<Category>().ToListAsync(cancellationToken);

        var categoryDictionary = categories.ToDictionary(l => l.Id);

        foreach (var categoryItem in categoryDictionary)
        {
            if (categoryDictionary.TryGetValue(product.CategoryId, out var category))
            {
                product.Category = category.Name;
            }
        }

        return product;
    }

    public async Task Update(Product product)
    {
        _session.Update(product);
        await _session.SaveChangesAsync();
    }
}
