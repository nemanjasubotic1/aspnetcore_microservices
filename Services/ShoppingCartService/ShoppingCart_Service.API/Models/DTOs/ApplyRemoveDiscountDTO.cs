using Main.ShoppingCartService.ShoppingCart_Service.API.Models;

namespace ShoppingCart_Service.API.Models.DTOs;

public class ApplyRemoveDiscountDTO
{
    public Guid Id { get; set; }
    public string CouponName { get; set; } = string.Empty;
    public decimal Discount { get; set; }
}
