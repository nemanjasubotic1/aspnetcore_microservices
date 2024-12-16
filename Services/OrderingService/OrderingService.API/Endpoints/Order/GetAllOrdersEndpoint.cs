using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.OrderingService.OrderingService.Application;
using Services.OrderingService.OrderingService.Application.Orders.Queries.GetAllOrders;

namespace Services.OrderingService.OrderingService.API.Endpoints.Order;

public class GetAllOrdersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/order", async ( [FromBody] GetAllOrdersQuery request,ISender sender) =>
        {
            var result = await sender.Send(request);

            return Results.Ok(result);


        }).RequireAuthorization()
        .WithName("GetAllOrders")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetAllOrders")
        .WithDescription("Serve for changins get the list of all orders");
    }
}
