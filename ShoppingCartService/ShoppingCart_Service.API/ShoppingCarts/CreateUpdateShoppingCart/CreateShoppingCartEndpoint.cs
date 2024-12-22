using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.ShoppingCart_Service.API.Models.DTOs;

namespace Services.ShoppingCart_Service.API.ShoppingCarts.CreateUpdateShoppingCart;

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
