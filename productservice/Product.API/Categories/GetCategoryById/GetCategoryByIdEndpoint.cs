using Carter;
using MediatR;
using ProductCategory.API.Categories.GetAllCategories;

namespace ProductCategory.API.Categories.GetCategoryById;

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
        });
    }
}
