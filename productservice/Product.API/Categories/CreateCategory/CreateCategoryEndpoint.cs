using Carter;
using GeneralUsing.CQRS;
using Mapster;
using MediatR;

namespace ProductCategory.API.Categories.CreateCategory;

public record CreateCategoryRequest(string Name, string Description);
public record CreateCategoryResponse(Guid Id);



public class CreateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/category", async (CreateCategoryRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateCategoryCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateCategoryResponse>();

            return Results.Created($"/category/{response.Id}", response);
        });
    }
}
