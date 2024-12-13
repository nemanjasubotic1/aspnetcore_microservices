using Carter;
using MediatR;

namespace ProductCategory.API.Categories.GetCategoryById;

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
        });
    }
}
