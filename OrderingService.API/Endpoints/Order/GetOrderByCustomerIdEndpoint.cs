using Carter;
using MediatR;
using OrderingService.Application.Orders.Queries.GetOrderByCustomerId;

namespace OrderingService.API.Endpoints.Order;

//public record GetOrderByCustomerIdQuery(Guid CustomerId) : IQuery<GetOrderByCustimerIdResult>;
//public record GetOrderByCustimerIdResponse(OrderHeaderDTO? OrderHeaderDTO = null);

public class GetOrderByCustomerIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/order/{CustomerId}", async (Guid CustomerId, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderByCustomerIdQuery(CustomerId));


            if (result.Errors != null && result.Errors.Any())
            {
                return Results.BadRequest(result.Errors.Select(l => l.ErrorMessage));
            }

            return Results.Ok(result);

        });
    }
}
