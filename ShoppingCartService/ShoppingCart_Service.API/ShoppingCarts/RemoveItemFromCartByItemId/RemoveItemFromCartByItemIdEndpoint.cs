using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart_Service.API.ShoppingCarts.RemoveItemFromCartByItemId;

public record RemoveItemFromCartByItemIdRequest(Guid id, string userId);

public class RemoveItemFromCartByItemIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/shoppingcart/itemremove/", async ([FromBody] RemoveItemFromCartByItemIdRequest request, ISender sender) =>
        {
            var result = await sender.Send(new RemoveItemFromCartByItemIdCommand(request.id, request.userId));

            if (result.Errors != null && result.Errors.Any())
            {
                return Results.BadRequest(result.Errors.Select(l => l.ErrorMessage));
            }

            return Results.Ok(result);

        })
        .WithName("RemoveItemFromCart")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("RemoveItemFromCart")
        .WithDescription("Serve for removing single item from the cart");
    }
}
