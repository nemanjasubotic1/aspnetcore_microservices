﻿using GeneralUsing.CQRS;
using Main.ShoppingCartService.ShoppingCart_Service.API.Data;

namespace Main.ShoppingCartService.ShoppingCart_Service.API.ShoppingCarts.DeleteShoppingCart;

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
