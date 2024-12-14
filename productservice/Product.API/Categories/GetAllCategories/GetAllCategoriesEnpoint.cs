using Carter;
using Mapster;
using MediatR;
using ProductCategory.API.Models;

namespace ProductCategory.API.Categories.GetAllCategories;

public record GetAllCategoriesRequest(int? PageNumber = 1, int? PageSize = 10);
//public record GetAllCategoriesResponse(IEnumerable<Category> Categories);

public class GetAllCategoriesEnpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/category", async ([AsParameters] GetAllCategoriesRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetAllCategoriesQuery>();

            var result = await sender.Send(query);

            //var response = result.Adapt<GetAllCategoriesResponse>();

            return Results.Ok(result);
        }).RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("GetCateogries")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetCateogries")
        .WithDescription("Serve for getting all categories");
    }
}
