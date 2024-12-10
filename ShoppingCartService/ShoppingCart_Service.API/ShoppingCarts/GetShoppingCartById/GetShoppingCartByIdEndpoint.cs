using Carter;
using Mapster;
using MediatR;
using ShoppingCart_Service.API.Models.DTOs;

namespace ShoppingCart_Service.API.ShoppingCarts.GetShoppingCartById;


//public record GetShoppingCartByIdRequest(Guid Id);
public record GetShoppingCartByIdResponse(ShoppingCartDTO ShoppingCartDTO);

public class GetShoppingCartByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/shoppingcart/{Id}", async (Guid Id, ISender sender ) =>
        {
            var query = new GetShoppingCartByIdQuery(Id);

            var result = await sender.Send(query);

            var response = new GetShoppingCartByIdResponse(result.ShoppingCartDTO);

            return Results.Ok(response);
        });
    }
}
