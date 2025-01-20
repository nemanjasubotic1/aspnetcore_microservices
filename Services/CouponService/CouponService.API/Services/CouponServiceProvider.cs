using CouponService.Api.Protos;
using CouponService.API.Data;
using CouponService.API.Models;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CouponService.API.Services;

public class CouponServiceProvider : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly AppDbContext _db;
    public CouponServiceProvider(AppDbContext db)
    {
        _db = db;
    }
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = new Coupon
        {
            CouponName = request.Coupon.Name,
            Amount = decimal.Parse(request.Coupon.Amount),
            ExpiryDate = request.Coupon.ExpiryDate.ToDateTime(),
        };

        if (coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        await _db.Coupons.AddAsync(coupon);
        await _db.SaveChangesAsync();

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _db.Coupons.FirstOrDefaultAsync(l => l.CouponName == request.Name);

        if (coupon is null)
        {
            return new CouponModel();
        }

        var couponModel = ToCouponModel(coupon);

        return couponModel;
    }

    public override async Task<GetAllDiscountsResponse> GetAllDiscounts(GetAllDiscountsRequest request, ServerCallContext context)
    {
        List<Coupon> couponsList = await _db.Coupons.ToListAsync();

        if (couponsList is null)
        {
            return new GetAllDiscountsResponse();
        }

        var coupons = couponsList.Select(coupon => ToCouponModel(coupon)).ToList();

        return new GetAllDiscountsResponse { Coupons = { coupons } };

    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        _db.Coupons.Update(ToCoupon(request.Coupon));
        await _db.SaveChangesAsync();

        return request.Coupon;
    }

    private static CouponModel ToCouponModel(Coupon coupon)
    {
        return new CouponModel
        {
            Id = coupon.Id,
            Name = coupon.CouponName,
            Amount = coupon.Amount.ToString(),
            ExpiryDate = Timestamp.FromDateTime(coupon.ExpiryDate.ToUniversalTime()),
            IsEnabled = coupon.IsEnabled
        };
    }

    private static Coupon ToCoupon(CouponModel couponModel)
    {
        return new Coupon
        {
            Id = couponModel.Id,
            CouponName = couponModel.Name,
            Amount = Convert.ToDecimal(couponModel.Amount),
            ExpiryDate = couponModel.ExpiryDate.ToDateTime(),
            IsEnabled = couponModel.IsEnabled
        };
    }
}
