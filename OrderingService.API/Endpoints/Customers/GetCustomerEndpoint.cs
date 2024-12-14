using Carter;
using Mapster;
using MediatR;
using OrderingService.Application.Customers.Queries.GetCustomerById;
using OrderingService.Application.DTOs;

namespace OrderingService.API.Endpoints.Customers;

//public record GetCustomerByIdRequest(Guid Id);
//public record GetCustomerByIdResponse(bool DoesExist, CustomerDTO? CustomerDTO = null);

public class GetCustomerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/customer/{Id}", async (Guid Id, ISender sender) =>
        {

            var query = await sender.Send(new GetCustomerByIdQuery(Id));

            //var response = query.Adapt<GetCustomerByIdResponse>();

            return Results.Ok(query);

        });
    }
}
