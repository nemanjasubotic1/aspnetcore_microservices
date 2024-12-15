using Carter;
using MediatR;
using OrderingService.Application.Orders.Queries.GetAllOrders;

namespace OrderingService.API.Endpoints.Order;

public class GetAllOrdersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/order", async (ISender sender) =>
        {
            var result = await sender.Send(new GetAllOrdersQuery());

            return Results.Ok(result);
        });
    }
}
