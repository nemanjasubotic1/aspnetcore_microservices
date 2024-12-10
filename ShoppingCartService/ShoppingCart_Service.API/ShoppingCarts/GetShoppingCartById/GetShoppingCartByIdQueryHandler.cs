using GeneralUsing.CQRS;
using GeneralUsing.Exceptions.CustomExceptionHandlers;
using Mapster;
using ShoppingCart_Service.API.Data;
using ShoppingCart_Service.API.Models.DTOs;

namespace ShoppingCart_Service.API.ShoppingCarts.GetShoppingCartById;

public record GetShoppingCartByIdQuery(Guid Id) : IQuery<GetShoppingCartByIdResult>;
public record GetShoppingCartByIdResult(ShoppingCartDTO ShoppingCartDTO);


public class GetShoppingCartByIdQueryHandler(IShoppingCartRepository shoppingCartRepository) : IQueryHandler<GetShoppingCartByIdQuery, GetShoppingCartByIdResult>
{
    public async Task<GetShoppingCartByIdResult> Handle(GetShoppingCartByIdQuery query, CancellationToken cancellationToken)
    {
        var shoppingCart = await shoppingCartRepository.GetShoppingCartByIdAsync(query.Id, cancellationToken);

        if (shoppingCart == null)
        {
            throw new NotFoundException($"The cart with id {query.Id} was not found");
        }

        return new GetShoppingCartByIdResult(shoppingCart.Adapt<ShoppingCartDTO>());
    }
}
