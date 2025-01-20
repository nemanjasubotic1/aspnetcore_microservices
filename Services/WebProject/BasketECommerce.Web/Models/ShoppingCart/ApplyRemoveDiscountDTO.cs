namespace BasketECommerce.Web.Models.ShoppingCart;

public class ApplyRemoveDiscountDTO
{
    public Guid Id { get; set; }
    public string CouponName { get; set; } = string.Empty;
    public decimal Discount { get; set; }
}
