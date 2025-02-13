﻿using Carter;
using MediatR;

namespace Main.ShoppingCartService.ShoppingCart_Service.API.ShoppingCarts.GetShoppingCartById;


//public record GetShoppingCartByIdRequest(Guid Id);
//public record GetShoppingCartByIdResponse(ShoppingCartDTO ShoppingCartDTO);

public class GetShoppingCartByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/shoppingcart/{Id}", async (Guid Id, ISender sender ) =>
        {
            var query = new GetShoppingCartByIdQuery(Id);

            var result = await sender.Send(query);

            //var response = new GetShoppingCartByIdResponse(result.ShoppingCartDTO);

            return Results.Ok(result);
        }).RequireAuthorization()
        .WithName("GetShoppingCartById")
        .Produces<CustomApiResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetShoppingCartById")
        .WithDescription("Get the cart details by cart id.");
    }
}
