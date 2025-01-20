using Carter;
using Main.ShoppingCartService.ShoppingCart_Service.API.Models.DTOs;
using MediatR;
using ShoppingCart_Service.API.Models.DTOs;

namespace Main.ShoppingCartService.ShoppingCart_Service.API.ShoppingCarts.EmailShoppingCart;

public record EmailShoppingCartRequest(EmailCartDTO EmailCartDTO);
//public record EmailShoppingCartResponse(bool IsSucces);

public class EmailShoppingCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/shoppingcart/emailnotification", async (EmailShoppingCartRequest request, ISender sender) =>
        {
            var command = new EmailShoppingCartCommand(request.EmailCartDTO);

            var result = await sender.Send(command);

            if (result.Errors != null && result.Errors.Any())
            {
                return Results.BadRequest(result.Errors.Select(l => l.ErrorMessage));
            }

            return Results.Ok(result);
        })
        .WithName("EmailShoppingCart")
        .Produces<CustomApiResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("EmailShoppingCart")
        .WithDescription("Email the cart details.");
    }
}
