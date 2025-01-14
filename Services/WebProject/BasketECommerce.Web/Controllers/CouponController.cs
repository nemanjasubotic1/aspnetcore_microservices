using BasketECommerce.Web.Models.Coupons;
using BasketECommerce.Web.Services.CouponService;
using Microsoft.AspNetCore.Mvc;

namespace BasketECommerce.Web.Controllers;
public class CouponController : Controller
{
    private readonly IDiscountService _discountService;

    public CouponController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }


    [HttpGet]
    public IActionResult CreateCoupon()
    {
        CouponModelDTO couponModelDTO = new();

        couponModelDTO.ExpiryDate = DateTime.Now;

        return View(couponModelDTO);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCoupon(CouponModelDTO couponModelDTO)
    {
        if (ModelState.IsValid)
        {
            var createdCouponModelDto = await _discountService.CreateCoupon(couponModelDTO);

            if (createdCouponModelDto == null)
            {
                TempData["error"] = "Error creating coupon";
                return View(couponModelDTO);
            }
        }
        else
        {
            TempData["error"] = "Error creating coupon";
            return View(couponModelDTO);
        }

        TempData["success"] = "Successfully created new Coupon";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> EnableDisable(string couponName)
    {
        var couponModelDto = await _discountService.UpdateCouponStatus(couponName);

        if (string.IsNullOrEmpty(couponModelDto.CouponName))
        {
            TempData["error"] = "Error changing status";
            return RedirectToAction(nameof(Index));
        }

        TempData["success"] = $"Coupon id is {couponName}";

        return RedirectToAction(nameof(Index));
    }


    #region APICALLS

    public async Task<IActionResult> GetAllCoupons()
    {
        var couponModelDtoList = await _discountService.GetAllCoupons();

        return Json(new { coupons = couponModelDtoList });
    }

    #endregion
}
