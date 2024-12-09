using Carter;
using Mapster;
using MediatR;
using ProductCategory.API.Models;

namespace ProductCategory.API.Categories.GetAllCategories;

public record GetAllCategoriesRequest(int? PageNumber = 1, int? PageSize = 1);
public record GetAllCategoriesResponse(IEnumerable<Category> Categories);

public class GetAllCategoriesEnpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/category", async ([AsParameters] GetAllCategoriesRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetAllCategoriesQuery>();

            var result = await sender.Send(query);

            var response = result.Adapt<GetAllCategoriesResponse>();

            return Results.Ok(response);
        });
    }
}
