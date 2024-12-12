using Marten;
using ProductCategory.API.Data.General;
using ProductCategory.API.Models;

namespace ProductCategory.API.Data;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly IDocumentSession _session;
    public CategoryRepository(IDocumentSession session) : base(session)
    {
        _session = session;
    }

    public async Task<List<Category>> GetAllCategoriesWithProducts(CancellationToken cancellationToken, int? pageNumber = null, int? pageSize = null)
    {
        var categories = await GetAllAsync<Category>(filter:null, isPaged: true, pageNumber ?? 1, pageSize ?? 10, cancellationToken);

        var allProducts = await _session.Query<Product>().ToListAsync(cancellationToken);

        var productDictionary = allProducts.GroupBy(l => l.CategoryId).ToDictionary(l => l.Key, l => l.ToList());

        foreach (var category in categories)
        {
            if (productDictionary.TryGetValue(category.Id, out var products))
            {
                category.Products.AddRange(products);
            }
        }

        return categories.ToList();
    }

    public async Task Update(Category category)
    {
        _session.Update(category);
        await _session.SaveChangesAsync();
    }
}
