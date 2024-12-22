namespace Main.ProductService.ProductCategory.API.InitialData;

public class CategoryDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}
