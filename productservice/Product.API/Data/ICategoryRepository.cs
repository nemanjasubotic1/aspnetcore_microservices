using ProductCategory.API.Data.General;
using ProductCategory.API.Models;

namespace ProductCategory.API.Data;

public interface ICategoryRepository : IRepository<Category>
{
    Task Update(Category category);
    Task<List<Category>> GetAllCategoriesWithProducts(CancellationToken cancellationToken, int? pageNumber = null, int? pageSize = null);
}
