using System.ComponentModel.DataAnnotations;

namespace BasketECommerce.Web.Models.ProductCategory;

public class ProductModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; } 
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public DateOnly YearOfProduction { get; set; } 
    public Guid CategoryId { get; set; }
    public string Category { get; set; }




 
}
