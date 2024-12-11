using Carter;
using GeneralUsing.CQRS;
using Mapster;
using MediatR;
using Microsoft.Identity.Client;
using OrderingService.Application.DTOs;
using OrderingService.Application.Orders.Queries.GetOrderByCustomerId;

namespace OrderingService.API.Endpoints.Order;

//public record GetOrderByCustomerIdQuery(Guid CustomerId) : IQuery<GetOrderByCustimerIdResult>;
public record GetOrderByCustimerIdResponse(OrderHeaderDTO? OrderHeaderDTO = null);

public class GetOrderByCustomerIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/order/{CustomerId}", async (Guid CustomerId, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderByCustomerIdQuery(CustomerId));

            var response = new GetOrderByCustimerIdResponse(result.OrderHeaderDTO);

            return Results.Ok(response);

        });
    }
}
