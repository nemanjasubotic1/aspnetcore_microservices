using Carter;
using FluentValidation;
using GeneralUsing.CQRS;
using Mapster;
using MediatR;
using ProductCategory.API.Models.DTOs;

namespace ProductCategory.API.Categories.UpdateCategory;

public record UpdateCategoryRequest(CategoryDTO CategoryDTO) : ICommand<CustomApiResponse>;
//public record UpdateCategoryResponse(Guid Id);

public class UpdateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/category", async (UpdateCategoryRequest request, ISender sender) =>
        {
            var command = new UpdateCategoryCommand(request.CategoryDTO);

            var result = await sender.Send(command);

            //var response = result.Adapt<UpdateCategoryResponse>();

            return Results.Ok(result);

        });
    }
}
