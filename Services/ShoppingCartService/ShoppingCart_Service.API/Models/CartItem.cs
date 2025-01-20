
namespace Main.ShoppingCartService.ShoppingCart_Service.API.Models;

public class CartItem : IBaseModel
{
    public Guid Id { get; set; } // real product id
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public DateOnly YearOfProduction { get; set; }
}
