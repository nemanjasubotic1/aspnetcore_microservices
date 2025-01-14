using BasketECommerce.Web.Models.Coupons;
using CouponService.Api.Protos;
using Google.Protobuf.WellKnownTypes;

namespace BasketECommerce.Web.Services.CouponService;

public class DiscountService : IDiscountService
{
    private DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;
    public DiscountService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
    {
        _discountProtoServiceClient = discountProtoServiceClient;
    }
    public async Task<CouponModelDTO> CreateCoupon(CouponModelDTO couponModelDto)
    {
        var couponModel = await _discountProtoServiceClient.CreateDiscountAsync(new CreateDiscountRequest { Coupon = ToCouponModel(couponModelDto)});

        return ToCouponDto(couponModel);
    }

    public async Task<CouponModelDTO> UpdateCouponStatus(int id)
    {
        var couponModelDto = await GetCoupon(id);

        if (couponModelDto == null)
        {
            return new CouponModelDTO();
        }

        couponModelDto.IsEnabled = !couponModelDto.IsEnabled;

        var couponModel = await _discountProtoServiceClient.UpdateDiscountAsync(new UpdateDiscountRequest {  Coupon = ToCouponModel(couponModelDto) });

        return ToCouponDto(couponModel);
    }

    public async Task<CouponModelDTO> GetCoupon(int id)
    {
        var couponModel = await _discountProtoServiceClient.GetDiscountAsync(new GetDiscountRequest { Id = id });

        CouponModelDTO couponModelDTO = ToCouponDto(couponModel);

        return couponModelDTO;
    }

    public async Task<List<CouponModelDTO>> GetAllCoupons()
    {
        var couponModelList = await _discountProtoServiceClient.GetAllDiscountsAsync(new GetAllDiscountsRequest());

        var couponModelDTOList = couponModelList.Coupons.Select(coupon => ToCouponDto(coupon)).ToList();

        return couponModelDTOList;
    }

    private static CouponModelDTO ToCouponDto(CouponModel couponModel)
    {
        var expiryDate = couponModel.ExpiryDate.ToDateTime();

        return new CouponModelDTO
        {
            Id = couponModel.Id,
            CouponName = couponModel.Name,
            Amount = Convert.ToDecimal(couponModel.Amount),
            ExpiryDate = expiryDate,
            IsEnabled = couponModel.IsEnabled,
            IsExpired = expiryDate <= DateTime.Now,
        };
    }

    private static CouponModel ToCouponModel(CouponModelDTO couponModelDTO)
    {
        return new CouponModel
        {
            Id= couponModelDTO.Id,
            Name = couponModelDTO.CouponName,
            Amount = couponModelDTO.Amount.ToString(),
            IsEnabled = couponModelDTO.IsEnabled,
            ExpiryDate = Timestamp.FromDateTime(couponModelDTO.ExpiryDate.ToUniversalTime()),
        };
    }

   
}
