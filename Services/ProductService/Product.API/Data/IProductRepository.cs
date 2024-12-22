using Services.ProductService.ProductCategory.API.Data.General;
using Services.ProductService.ProductCategory.API.Models;

namespace Services.ProductService.ProductCategory.API.Data;

public interface IProductRepository : IRepository<Product>
{
    Task Update(Product product);
    Task<List<Product>> GetAllProductsWithCategory(CancellationToken cancellationToken, int? pageNumber = null, int? pageSize = null);
    Task<Product> GetProductByIdWithCategory(Guid Id, CancellationToken cancellationToken);
}
