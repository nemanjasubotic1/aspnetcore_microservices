using Carter;
using MediatR;
using OrderingService.Application;
using OrderingService.Application.Orders.Queries.GetOrderByCustomerId;

namespace OrderingService.API.Endpoints.Order;

//public record GetOrderByCustomerIdQuery(Guid CustomerId) : IQuery<GetOrderByCustimerIdResult>;
//public record GetOrderByCustimerIdResponse(OrderHeaderDTO? OrderHeaderDTO = null);

public class GetOrderByUserIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/order/{UserId}", async (Guid UserId, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderByUserIdQuery(UserId));


            if (result.Errors != null && result.Errors.Any())
            {
                return Results.BadRequest(result.Errors.Select(l => l.ErrorMessage));
            }

            return Results.Ok(result);

        }).RequireAuthorization()
        .WithName("GetOrderByUserId")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetOrderByUserId")
        .WithDescription("Serve for changins get order by UserId");
    }
}
