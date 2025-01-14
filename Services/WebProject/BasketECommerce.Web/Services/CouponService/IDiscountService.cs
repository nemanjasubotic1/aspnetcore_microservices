using BasketECommerce.Web.Models.Coupons;

namespace BasketECommerce.Web.Services.CouponService;

public interface IDiscountService
{
    Task<CouponModelDTO> CreateCoupon(CouponModelDTO couponModel);
    Task<CouponModelDTO> UpdateCouponStatus(string name);
    Task<CouponModelDTO> GetCoupon(string couponName);
    Task<List<CouponModelDTO>> GetAllCoupons();

}
