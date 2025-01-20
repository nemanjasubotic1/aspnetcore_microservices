using BasketECommerce.Web.Models.ProductCategory;
using System.Reflection.Metadata.Ecma335;

namespace BasketECommerce.Web.Models.ShoppingCart;

public class CartItemModel
{
    public Guid Id { get; set; } // real product id
    //public ProductModel ProductModel{ get; set; }
    public string ProductName { get; set; }
    
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public DateOnly YearOfProduction { get; set; }
}
