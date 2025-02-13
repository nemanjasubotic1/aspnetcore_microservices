﻿using GeneralUsing.CQRS;
using GeneralUsing.Exceptions.CustomExceptionHandlers;
using Main.ShoppingCartService.ShoppingCart_Service.API.Data;
using Main.ShoppingCartService.ShoppingCart_Service.API.Models.DTOs;
using Mapster;

namespace Main.ShoppingCartService.ShoppingCart_Service.API.ShoppingCarts.GetShoppingCartById;

public record GetShoppingCartByIdQuery(Guid Id) : IQuery<CustomApiResponse>;
//public record GetShoppingCartByIdResult(ShoppingCartDTO ShoppingCartDTO);


public class GetShoppingCartByIdQueryHandler(IShoppingCartRepository shoppingCartRepository) : IQueryHandler<GetShoppingCartByIdQuery, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(GetShoppingCartByIdQuery query, CancellationToken cancellationToken)
    {
        var shoppingCart = await shoppingCartRepository.GetShoppingCartByIdAsync(query.Id, cancellationToken);

        if (shoppingCart == null)
        {
            throw new NotFoundException($"The cart with id {query.Id} was not found");
        }

        return new CustomApiResponse(shoppingCart.Adapt<ShoppingCartDTO>());
    }
}
