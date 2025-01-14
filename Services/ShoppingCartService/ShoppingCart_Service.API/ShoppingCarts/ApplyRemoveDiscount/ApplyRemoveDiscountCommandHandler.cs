using GeneralUsing.CQRS;
using Main.ShoppingCartService.ShoppingCart_Service.API;
using Main.ShoppingCartService.ShoppingCart_Service.API.Data;
using ShoppingCart_Service.API.Models.DTOs;

namespace ShoppingCart_Service.API.ShoppingCarts.ApplyRemoveDiscount;

public record ApplyRemoveDiscountCommand(ApplyRemoveDiscountDTO ApplyRemoveDiscountDTO) : ICommand<CustomApiResponse>;

public class ApplyRemoveDiscountCommandHandler(IShoppingCartRepository shoppingCartRepository) : ICommandHandler<ApplyRemoveDiscountCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(ApplyRemoveDiscountCommand request, CancellationToken cancellationToken)
    {
        var cartFromDb = await shoppingCartRepository.GetShoppingCartByIdAsync(request.ApplyRemoveDiscountDTO.Id, cancellationToken);

        cartFromDb.CouponName = request.ApplyRemoveDiscountDTO.CouponName;
        cartFromDb.Discount = request.ApplyRemoveDiscountDTO.Discount;

        await shoppingCartRepository.UpdateShoppingCart(cartFromDb, cancellationToken);

        return new CustomApiResponse(cartFromDb.Id);
    }
}
