using Marten;
using Marten.Pagination;
using ProductCategory.API.Data.General;
using ProductCategory.API.Models;

namespace ProductCategory.API.Data;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly IDocumentSession _session;

    public ProductRepository(IDocumentSession session) : base(session)
    {
        _session = session;
    }

    public async Task<List<Product>> GetAllProductsWithCategory(CancellationToken cancellationToken, int? pageNumber = null, int? pageSize = null)
    {
        var allProducts = await GetAllAsync<Product>(filter: null, isPaged: true, pageNumber ?? 1, pageSize ?? 1, cancellationToken);

        var categoryIds = allProducts.Select(x => x.CategoryId).Distinct().ToList();

        var categories = await _session.Query<Category>().Where(l => categoryIds.Contains(l.Id)).ToListAsync();

        var categoryDictionary = categories.ToDictionary(l => l.Id);

        foreach (var product in allProducts)
        {
            if (categoryDictionary.TryGetValue(product.CategoryId, out var category))
            {
                product.Category = category.Name;
            }
        }

        return allProducts.ToList();
    }

    public async Task Update(Product product)
    {
        _session.Update(product);
        await _session.SaveChangesAsync();
    }
}
