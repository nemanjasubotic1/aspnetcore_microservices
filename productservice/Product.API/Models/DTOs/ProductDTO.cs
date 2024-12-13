namespace ProductCategory.API.Models.DTOs;

public class ProductDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public string? ImageUrl { get; set; } = default!;
    public DateOnly YearOfProduction { get; set; } = default!;
    public Guid CategoryId { get; set; } = default!;
    public Category Category { get; set; } = default!;
}
