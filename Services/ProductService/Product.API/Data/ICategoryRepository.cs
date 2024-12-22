using Services.ProductService.ProductCategory.API.Data.General;
using Services.ProductService.ProductCategory.API.Models;

namespace Services.ProductService.ProductCategory.API.Data;

public interface ICategoryRepository : IRepository<Category>
{
    Task Update(Category category);
    Task<List<Category>> GetAllCategoriesWithProducts(CancellationToken cancellationToken, int? pageNumber = null, int? pageSize = null);
}
