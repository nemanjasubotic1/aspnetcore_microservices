using Carter;
using MediatR;
namespace Main.ProductService.ProductCategory.API.InitialData;

public record GetAllProductsRequest(int? pageNumber = 1, int? pageSize = 10);
//public record GetAllProductsResponse(IEnumerable<Product> Products);

public class GetAllProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product", async ([AsParameters] GetAllProductsRequest request, ISender sender) =>
        {
            var query = new GetAllProductsQuery(request.pageNumber, request.pageSize);

            var result = await sender.Send(query);

            //var response = result.Adapt<GetAllProductsResponse>();

            return Results.Ok(result);
        })
        .WithName("GetAllProducts")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetAllProducts")
        .WithDescription("Serve for getting all products");
    }
}
