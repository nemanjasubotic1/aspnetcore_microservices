using Carter;
using Mapster;
using MediatR;
using OrderingService.Application.Customers.Commands.CreateCustomer;
using OrderingService.Application.DTOs;

namespace OrderingService.API.Endpoints.Customer;

public record CreateCustomerRequest(CustomerDTO CustomerDTO);
public record CreateCustomerResponse(Guid Id);


public class CreateCustomerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/customer", async (CreateCustomerRequest request, ISender sender) =>
        {
            var commad = new CreateCustomerCommand(request.CustomerDTO);

            var result = await sender.Send(commad);

            var response = result.Adapt<CreateCustomerResponse>();

            return Results.Created($"/customer/{response.Id}", response);

        });
    }
}
