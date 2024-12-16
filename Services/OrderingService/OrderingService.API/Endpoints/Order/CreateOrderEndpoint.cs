using Carter;
using MediatR;
using OrderingService.Application.DTOs;
using Services.OrderingService.OrderingService.Application;
using Services.OrderingService.OrderingService.Application.DTOs;
using Services.OrderingService.OrderingService.Application.Orders.Commands.CreateOrder;

namespace Services.OrderingService.OrderingService.API.Endpoints.Order;

public record CreateOrderRequest(OrderHeaderDTO OrderHeaderDTO, CustomerDTO CustomerDTO);
//public record CreateOrderResponse(Guid Id);


public class CreateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/order", async (CreateOrderRequest request, ISender sender) =>
        {
            var command = new CreateOrderCommand(request.OrderHeaderDTO, request.CustomerDTO);

            var result = await sender.Send(command);

            //var response = result.Adapt<CreateOrderResponse>();

            return Results.Created($"/order/", result);

        }).RequireAuthorization()
        .WithName("CreateOrder")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("CreateOrder")
        .WithDescription("Serve for changins making the new order");
    }
}
