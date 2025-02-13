﻿using Carter;
using MediatR;
using Services.OrderingService.OrderingService.Application;
using Services.OrderingService.OrderingService.Application.Customers.Commands.CreateCustomer;
using Services.OrderingService.OrderingService.Application.DTOs;

namespace Services.OrderingService.OrderingService.API.Endpoints.Customers;

public record CreateCustomerRequest(CustomerDTO CustomerDTO);
//public record CreateCustomerResponse(Customer Customer);


public class CreateCustomerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/customer", async (CreateCustomerRequest request, ISender sender) =>
        {
            var commad = new CreateCustomerCommand(request.CustomerDTO);

            var result = await sender.Send(commad);

            //var response = result.Adapt<CreateCustomerResponse>();

            return Results.Created($"/customer/", result);

        }).RequireAuthorization()
        .WithName("CreateCustomer")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("CreateCustomer")
        .WithDescription("Serve for creating customer with creation of the order if customer dont exist");
    }
}
