namespace BasketECommerce.Web.Models.ShoppingCart;

public class ShoppingCartModel
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public List<CartItemModel> CartItems { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public decimal Discount { get; set; }
    public decimal CartTotal => CartItems.Sum(l => l.Price * l.Quantity) - Discount;
}


