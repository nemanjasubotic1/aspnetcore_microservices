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

    public async Task Update(Guid categoryId, Category category)
    {
        var categoryFromDb = await _session.LoadAsync<Category>(categoryId);

        categoryFromDb.Name = category.Name;
        categoryFromDb.Description = category.Description;

        _session.Store(categoryFromDb);
        await _session.SaveChangesAsync();
    }
}
