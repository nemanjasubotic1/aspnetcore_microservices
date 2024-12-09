using ProductCategory.API.Data.General;
using ProductCategory.API.Models;

namespace ProductCategory.API.Data;

public interface ICategoryRepository : IRepository<Category>
{
    Task Update(Guid categoryId, Category category);
}
