using Carter;
using Mapster;
using MediatR;

namespace ShoppingCart_Service.API.ShoppingCarts.DeleteShoppingCart;

//public record DeleteShoppingCartRequest(Guid Id);
public record DeleteShoppingCartResponse(bool IsSuccess);

public class DeleteShoppingCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/shoppingcart/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteShoppingCartCommand(id));

            var response = result.Adapt<DeleteShoppingCartResponse>();

            return Results.Ok(response);
        });
    }
}
