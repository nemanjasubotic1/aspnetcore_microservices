using Carter;
using MediatR;

namespace Services.ProductService.ProductCategory.API.Categories.GetCategoryById;

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product/{Id}", async (Guid Id, ISender sender) =>
        {
            var query = new GetProductByIdQuery(Id);

            var result = await sender.Send(query);

            //var response = result.Adapt<GetAllCategoriesResponse>();

            return Results.Ok(result);
        })
        .WithName("GetProduct")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetProduct")
        .WithDescription("Serve for getting single product");
    }
}
