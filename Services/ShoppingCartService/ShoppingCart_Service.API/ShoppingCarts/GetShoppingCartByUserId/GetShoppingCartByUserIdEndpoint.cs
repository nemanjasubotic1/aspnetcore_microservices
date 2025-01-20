using Carter;
using MediatR;

namespace Main.ShoppingCartService.ShoppingCart_Service.API.ShoppingCarts.GetShoppingCartById;


//public record GetShoppingCartByIdRequest(Guid Id);
//public record GetShoppingCartByIdResponse(ShoppingCartDTO ShoppingCartDTO);

public class GetShoppingCartByUserIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/shoppingcart/usercart/{Id}", async (string Id, ISender sender ) =>
        {
            var query = new GetShoppingCartByUserIdQuery(Id);

            var result = await sender.Send(query);

            //var response = new GetShoppingCartByIdResponse(result.ShoppingCartDTO);

            return Results.Ok(result);

        }).RequireAuthorization()
        .WithName("GetShoppingCartByUserId")
        .Produces<CustomApiResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetShoppingCartByUserId")
        .WithDescription("Get the cart details by user id.");
    }
}
