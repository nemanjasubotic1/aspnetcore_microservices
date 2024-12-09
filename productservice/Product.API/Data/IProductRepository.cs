using Marten;
using ProductCategory.API.Data.General;
using ProductCategory.API.Models;

namespace ProductCategory.API.Data;

public interface IProductRepository : IRepository<Product>
{
    Task Update(Product product);
    Task<List<Product>> GetAllProductsWithCategory(CancellationToken cancellationToken, int? pageNumber = null, int? pageSize = null);
}
