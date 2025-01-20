using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.OrderingService.OrderingService.Application;
using Services.OrderingService.OrderingService.Application.Orders.Commands.ChangeOrderStatus;

namespace Services.OrderingService.OrderingService.API.Endpoints.Order;

public record ChangeOrderStatusRequest(Guid Id, string status);

public class ChangeOrderStatusEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("order/status", async ([FromBody] ChangeOrderStatusRequest request, ISender sender) =>
        {
            var result = await sender.Send(new ChangeOrderStatusCommand(request.Id, request.status));

            if (result.Errors != null && result.Errors.Any())
            {
                return Results.BadRequest(result.Errors.Select(l => l.ErrorMessage));
            }

            return Results.Ok(result);
        }).RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("ChangeOrderStatus")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("ChangeOrderStatus")
        .WithDescription("Serve for changins status of the order");
    }
}
