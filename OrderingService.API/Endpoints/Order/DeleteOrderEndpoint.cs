using Carter;
using GeneralUsing.CQRS;
using Mapster;
using MediatR;
using OrderingService.Application;
using OrderingService.Application.Orders.Commands.DeleteOrder;

namespace OrderingService.API.Endpoints.Order;

//public record DeleteOrderCommand(Guid Id) : ICommand<DeleteOrderResult>;
public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/order/{Id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteOrderCommand(Id));

            var response = result.Adapt<DeleteOrderResponse>();

            return Results.Ok(response);

        }).RequireAuthorization()
        .WithName("DeleteOrder")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("DeleteOrder")
        .WithDescription("Serve for changins deleting the order");
    }
}
