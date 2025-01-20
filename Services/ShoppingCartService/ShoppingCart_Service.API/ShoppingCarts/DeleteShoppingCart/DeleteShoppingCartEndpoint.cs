using Carter;
using MediatR;

namespace Main.ShoppingCartService.ShoppingCart_Service.API.ShoppingCarts.DeleteShoppingCart;

//public record DeleteShoppingCartRequest(Guid Id);
//public record DeleteShoppingCartResponse(bool IsSuccess);

public class DeleteShoppingCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/shoppingcart/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteShoppingCartCommand(id));

            return Results.Ok(result);
        }).RequireAuthorization()
        .WithName("DeleteShoppingCart")
        .Produces<CustomApiResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("DeleteShoppingCart")
        .WithDescription("Deleting the cart.");
    }
}
