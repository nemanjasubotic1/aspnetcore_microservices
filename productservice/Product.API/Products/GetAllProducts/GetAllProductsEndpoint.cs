using Carter;
using Mapster;
using MediatR;
using ProductCategory.API.Models;

namespace ProductCategory.API.Products.GetAllProducts;

public record GetAllProductsRequest(int? pageNumber = 1, int? pageSize = 1);
public record GetAllProductsResponse(IEnumerable<Product> Products);

public class GetAllProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product", async ([AsParameters] GetAllProductsRequest request, ISender sender) =>
        {
            var query = new GetAllProductsQuery(request.pageNumber, request.pageSize);

            var result = await sender.Send(query);

            var response = result.Adapt<GetAllProductsResponse>();

            return Results.Ok(response);
        });
    }
}
