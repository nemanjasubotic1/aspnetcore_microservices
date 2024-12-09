using Carter;
using Mapster;
using MediatR;

namespace ProductCategory.API.Categories.DeleteCategory;

//public record DeleteCategoryRequest(Guid Id);
public record DeleteCategoryResponse(bool IsSuccess);

public class DeleteCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/category/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteCategoryCommand(id));

            var response = result.Adapt<DeleteCategoryResponse>();

            return Results.Ok(response);

        });
    }
}
