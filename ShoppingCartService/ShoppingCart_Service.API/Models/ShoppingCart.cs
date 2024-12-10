namespace ShoppingCart_Service.API.Models;

public class ShoppingCart : IBaseModel
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public List<CartItem> CartItems { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public decimal Discount { get; set; }
    public decimal CartTotal => CartItems.Sum(l => l.Price * l.Quantity);
}

