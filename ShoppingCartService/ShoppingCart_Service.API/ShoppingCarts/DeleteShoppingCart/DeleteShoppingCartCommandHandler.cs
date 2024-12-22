using GeneralUsing.CQRS;
using Services.ShoppingCart_Service.API.Data;

namespace Services.ShoppingCart_Service.API.ShoppingCarts.DeleteShoppingCart;

public record DeleteShoppingCartCommand(Guid Id) : ICommand<CustomApiResponse>;
//public record DeleteShoppingCartResult(bool IsSuccess);

public class DeleteShoppingCartCommandHandler(IShoppingCartRepository shoppingCartRepository) : ICommandHandler<DeleteShoppingCartCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var isDeleted = await shoppingCartRepository.RemoveShoppingCart(request.Id, cancellationToken);

        return new CustomApiResponse(isDeleted);
    }
}
