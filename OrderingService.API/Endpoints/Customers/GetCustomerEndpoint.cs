using Carter;
using MediatR;
using OrderingService.Application;
using OrderingService.Application.Customers.Queries.GetCustomerById;

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

        }).RequireAuthorization()
        .WithName("GetCustomer")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetCustomer")
        .WithDescription("Serve for get the customer by Id");
    }
}
