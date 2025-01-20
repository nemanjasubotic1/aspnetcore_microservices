namespace CouponService.API.Models;

public class Coupon
{
    public int Id { get; set; }
    public string CouponName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsEnabled { get; set; } = true;
}
