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
        app.MapGet("/getallorders/{CustomerId?}", async (Guid? CustomerId, ISender sender) =>
        {
            GetAllOrdersQuery request = CustomerId == null ? new GetAllOrdersQuery() : new GetAllOrdersQuery(CustomerId);

            var result = await sender.Send(request);

            if (result.Errors != null && result.Errors.Any())
            {
                return Results.BadRequest(result.Errors.Select(l => l.ErrorMessage));
            }

            return Results.Ok(result);


        })
        .WithName("GetAllOrders")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetAllOrders")
        .WithDescription("Serve for changins get the list of all orders");
    }
}
