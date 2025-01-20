namespace Main.ProductService.ProductCategory.API.InitialData;

public interface ICategoryRepository : IRepository<Category>
{
    Task Update(Category category);
    Task<List<Category>> GetAllCategoriesWithProducts(CancellationToken cancellationToken, int? pageNumber = null, int? pageSize = null);
}
