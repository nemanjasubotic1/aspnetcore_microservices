namespace Services.ShoppingCartService.ShoppingCart_Service.API.Models.DTOs;

public class CartItemDTO
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
