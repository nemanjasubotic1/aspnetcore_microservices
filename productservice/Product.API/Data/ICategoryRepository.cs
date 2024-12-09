using ProductCategory.API.Data.General;
using ProductCategory.API.Models;

namespace ProductCategory.API.Data;

public interface ICategoryRepository : IRepository<Category>
{
    Task Update(Category category);
    Task<List<Category>> GetAllCategoriesWithProduts(CancellationToken cancellationToken, int? pageNumber = null, int? pageSize = null);
}
