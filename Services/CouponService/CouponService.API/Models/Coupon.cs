namespace CouponService.API.Models;

public class Coupon
{
    public int Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsExpired { get; set; } = false;
}
