using Carter;
using MediatR;

namespace Main.ProductService.ProductCategory.API.InitialData;

public class GetCategoryByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/category/{Id}", async (Guid Id, ISender sender) =>
        {
            var query = new GetCategoryByIdQuery(Id);

            var result = await sender.Send(query);

            //var response = result.Adapt<GetAllCategoriesResponse>();

            return Results.Ok(result);
        }).RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("GetCategory")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetCategory")
        .WithDescription("Serve for getting single category");
    }
}
