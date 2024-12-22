using FluentValidation.Results;
using GeneralUsing.CQRS;
using Services.ShoppingCart_Service.API.Data;

namespace Services.ShoppingCart_Service.API.ShoppingCarts.RemoveItemFromCartByItemId;

public record RemoveItemFromCartByItemIdCommand(Guid Id, string userId) : ICommand<CustomApiResponse>;

public class RemoveItemFromCartByItemIdCommandHandler(IShoppingCartRepository shoppingCartRepository) : ICommandHandler<RemoveItemFromCartByItemIdCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(RemoveItemFromCartByItemIdCommand request, CancellationToken cancellationToken)
    {
        var resultRemoveItem = await shoppingCartRepository.RemoveItemFromShoppingCart(request.Id, request.userId, cancellationToken);

        if (!resultRemoveItem)
        {
            return new CustomApiResponse(null, false, [new ValidationFailure("", "There are no cart item in the cart with that id")]);
        }

        return new CustomApiResponse();

    }
}
