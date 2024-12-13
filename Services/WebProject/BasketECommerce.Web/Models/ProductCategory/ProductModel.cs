using System.ComponentModel.DataAnnotations;

namespace BasketECommerce.Web.Models.ProductCategory;

public class ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public string? ImageUrl { get; set; } = default!;
    public DateOnly YearOfProduction { get; set; } = default!;
    public Guid CategoryId { get; set; }
    public string Category { get; set; }

    // wrapper models

    public record CreateProductRequest(ProductModel ProductDTO);
    public record UpdateProductRequest(ProductModel ProductDTO);
}
