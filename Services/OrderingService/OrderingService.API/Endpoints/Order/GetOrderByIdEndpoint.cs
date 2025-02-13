﻿using Carter;
using MediatR;
using Services.OrderingService.OrderingService.Application;
using Services.OrderingService.OrderingService.Application.Orders.Queries.GetOrderById;

namespace Services.OrderingService.OrderingService.API.Endpoints.Order;

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

        }).RequireAuthorization()
        .WithName("GetOrderById")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetOrderById")
        .WithDescription("Serve for changins get order by OrderId");
    }
}
