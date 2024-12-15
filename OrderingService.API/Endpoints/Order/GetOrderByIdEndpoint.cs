using Carter;
using MediatR;
using OrderingService.Application.Orders.Queries.GetOrderById;

namespace OrderingService.API.Endpoints.Order;

public class GetOrderByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/order/singleorder/{Id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderByIdQuery(Id));

            if (result.Errors != null && result.Errors.Any())
            {
                return Results.BadRequest(result.Errors.Select(l => l.ErrorMessage));
            }

            return Results.Ok(result);

        });
    }
}
