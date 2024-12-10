using GeneralUsing.CQRS;
using ShoppingCart_Service.API.Data;

namespace ShoppingCart_Service.API.ShoppingCarts.DeleteShoppingCart;

public record DeleteShoppingCartCommand(Guid Id) : ICommand<DeleteShoppingCartResult>;
public record DeleteShoppingCartResult(bool IsSuccess);

public class DeleteShoppingCartCommandHandler(IShoppingCartRepository shoppingCartRepository) : ICommandHandler<DeleteShoppingCartCommand, DeleteShoppingCartResult>
{
    public async Task<DeleteShoppingCartResult> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var isDeleted = await shoppingCartRepository.RemoveShoppingCart(request.Id, cancellationToken);

        return new DeleteShoppingCartResult(isDeleted);
    }
}
