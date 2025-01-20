using GeneralUsing.CQRS;
using GeneralUsing.Exceptions.CustomExceptionHandlers;
using Main.ShoppingCartService.ShoppingCart_Service.API.Data;
using Main.ShoppingCartService.ShoppingCart_Service.API.Models.DTOs;
using Mapster;

namespace Main.ShoppingCartService.ShoppingCart_Service.API.ShoppingCarts.GetShoppingCartById;

public record GetShoppingCartByUserIdQuery(string userId) : IQuery<CustomApiResponse>;
//public record GetShoppingCartByIdResult(ShoppingCartDTO ShoppingCartDTO);


public class GetShoppingCartByUserIdQueryHandler(IShoppingCartRepository shoppingCartRepository) : IQueryHandler<GetShoppingCartByUserIdQuery, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(GetShoppingCartByUserIdQuery query, CancellationToken cancellationToken)
    {
        var shoppingCart = await shoppingCartRepository.GetShoppingCartByUserIdAsync(query.userId, cancellationToken);

        if (shoppingCart == null)
        {
            throw new NotFoundException($"The cart for user with id {query.userId} was not found");
        }

        return new CustomApiResponse(shoppingCart.Adapt<ShoppingCartDTO>());
    }
}
