using Carter;
using Mapster;
using MediatR;
using OrderingService.Application.DTOs;
using OrderingService.Application.Orders.Commands.CreateOrder;

namespace OrderingService.API.Endpoints.Order;

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

        });
    }
}
