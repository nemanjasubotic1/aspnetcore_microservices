using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BasketECommerce.Web.Models.Coupons;

public class CouponModelDTO
{
    public int Id { get; set; }
    [Required]
    public string CouponName { get; set; }
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public DateTime ExpiryDate { get; set; }
    public bool IsEnabled { get; set; }
    public bool IsExpired { get; set; } = false;

}
