using BasketECommerce.Web.Models.Coupons;

namespace BasketECommerce.Web.Services.CouponService;

public interface IDiscountService
{
    Task<CouponModelDTO> CreateCoupon(CouponModelDTO couponModel);
    Task<CouponModelDTO> UpdateCouponStatus(int id);
    Task<CouponModelDTO> GetCoupon(int id);
    Task<List<CouponModelDTO>> GetAllCoupons();

}
