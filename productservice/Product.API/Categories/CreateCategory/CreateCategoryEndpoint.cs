using Carter;
using FluentValidation.Results;
using GeneralUsing.CQRS;
using Mapster;
using MediatR;
using ProductCategory.API.Models.DTOs;

namespace ProductCategory.API.Categories.CreateCategory;

public record CreateCategoryRequest(CategoryDTO CategoryDTO);
//public record CreateCategoryResponse(Guid Id, List<ValidationFailure>? Errors = null);



public class CreateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/category", async (CreateCategoryRequest request, ISender sender) =>
        {
            var command = new CreateCategoryCommand(request.CategoryDTO);

            var result = await sender.Send(command);
            
            //var response = result.Adapt<CreateCategoryResponse>();

            if (result.Errors != null && result.Errors.Any())
            {
                return Results.BadRequest(result.Errors.Select(l => l.ErrorMessage));
            }

            return Results.Created("/category", result);
        });
    }
}
