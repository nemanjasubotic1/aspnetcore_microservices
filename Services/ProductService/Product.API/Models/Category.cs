
namespace Services.ProductService.ProductCategory.API.Models;

public class Category : IBaseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<Product> Products { get; set; } = [];
}
