using Carter;
using Mapster;
using MediatR;
using ShoppingCart_Service.API.Models.DTOs;

namespace ShoppingCart_Service.API.ShoppingCarts.CreateUpdateShoppingCart;

public record CreateShoppingCartRequest(ShoppingCartDTO ShoppingCartDTO);
public record CreateShoppingCartResponse(Guid Id);


public class CreateShoppingCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/shoppingcart", async (CreateShoppingCartRequest request, ISender sender) =>
        {
            var command = new CreateShoppingCartCommand(request.ShoppingCartDTO);

            var result = await sender.Send(command);

            var response = result.Adapt<CreateShoppingCartResponse>();

            return Results.Created($"/shoppingcart/{response.Id}", response);

        });
    }
}
