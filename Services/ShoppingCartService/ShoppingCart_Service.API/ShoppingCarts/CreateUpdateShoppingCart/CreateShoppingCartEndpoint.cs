using Carter;
using Main.ShoppingCartService.ShoppingCart_Service.API.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Main.ShoppingCartService.ShoppingCart_Service.API.ShoppingCarts.CreateUpdateShoppingCart;

public record CreateShoppingCartRequest(ShoppingCartDTO ShoppingCartDTO);
//public record CreateShoppingCartResponse(Guid Id);


public class CreateShoppingCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/shoppingcart", async ([FromBody] CreateShoppingCartRequest request, ISender sender) =>
        {
            var command = new CreateShoppingCartCommand(request.ShoppingCartDTO);

            var result = await sender.Send(command);

            if (result.Errors != null && result.Errors.Any())
            {
                return Results.BadRequest(result.Errors.Select(l => l.ErrorMessage));
            }

            return Results.Created($"/shoppingcart", result);

        }).RequireAuthorization()
        .WithName("CreateShoppingCart")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("CreateShoppingCart")
        .WithDescription("Creating the cart.");
    }
}
