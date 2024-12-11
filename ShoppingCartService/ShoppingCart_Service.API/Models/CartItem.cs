
namespace ShoppingCart_Service.API.Models;

public class CartItem : IBaseModel
{
    public Guid Id { get; set; } // real product id
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
