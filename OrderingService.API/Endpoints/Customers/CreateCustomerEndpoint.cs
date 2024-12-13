using Carter;
using Mapster;
using MediatR;
using OrderingService.Application.Customers.Commands.CreateCustomer;
using OrderingService.Application.DTOs;
using OrderingService.Domain.Models;

namespace OrderingService.API.Endpoints.Customers;

public record CreateCustomerRequest(CustomerDTO CustomerDTO);
public record CreateCustomerResponse(Customer Customer);


public class CreateCustomerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/customer", async (CreateCustomerRequest request, ISender sender) =>
        {
            var commad = new CreateCustomerCommand(request.CustomerDTO);

            var result = await sender.Send(commad);

            var response = result.Adapt<CreateCustomerResponse>();

            return Results.Created($"/customer/{response.Customer.Id}", response);

        });
    }
}
