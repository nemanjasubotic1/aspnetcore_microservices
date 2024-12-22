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

            // try catch

            var result = await sender.Send(command);

            return Results.Created($"/shoppingcart", result);

        });
    }
}
