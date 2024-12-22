using Carter;
using Mapster;
using MediatR;

namespace Services.ProductService.ProductCategory.API.Categories.DeleteCategory;

//public record DeleteCategoryRequest(Guid Id);
//public record DeleteCategoryResponse(bool IsSuccess);

public class DeleteCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/category/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteCategoryCommand(id));

            //var response = result.Adapt<DeleteCategoryResponse>();

            return Results.Ok(result);

        }).RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("DeleteCategory")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("DeleteCategory")
        .WithDescription("Serve for getting deleting category");
    }
}
