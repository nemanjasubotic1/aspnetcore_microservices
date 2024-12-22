using Carter;
using Main.ShoppingCartService.ShoppingCart_Service.API.Models.DTOs;
using MediatR;

namespace Main.ShoppingCartService.ShoppingCart_Service.API.ShoppingCarts.EmailShoppingCart;

public record EmailShoppingCartRequest(ShoppingCartDTO ShoppingCartDTO);
//public record EmailShoppingCartResponse(bool IsSucces);

public class EmailShoppingCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/shoppingcart/emailnotification", async (EmailShoppingCartRequest request, ISender sender) =>
        {
            var command = new EmailShoppingCartCommand(request.ShoppingCartDTO);

            var result = await sender.Send(command);

            //var response = result.Adapt<EmailShoppingCartResponse>();

            return Results.Ok(result);
        });
    }
}
