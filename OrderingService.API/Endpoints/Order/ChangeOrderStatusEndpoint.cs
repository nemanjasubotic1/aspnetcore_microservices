using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Application.Orders.Commands.ChangeOrderStatus;

namespace OrderingService.API.Endpoints.Order;

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
        });
    }
}
