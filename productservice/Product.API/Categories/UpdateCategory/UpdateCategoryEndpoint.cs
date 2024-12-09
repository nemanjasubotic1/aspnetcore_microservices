using Carter;
using FluentValidation;
using GeneralUsing.CQRS;
using Mapster;
using MediatR;

namespace ProductCategory.API.Categories.UpdateCategory;

public record UpdateCategoryRequest(Guid Id, string Name, string Description) : ICommand<UpdateCategoryResponse>;
public record UpdateCategoryResponse(Guid Id);



public class UpdateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/category/{Id}", async (Guid Id, UpdateCategoryRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateCategoryCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateCategoryResponse>();

            return Results.Ok(response);

        });
    }
}
