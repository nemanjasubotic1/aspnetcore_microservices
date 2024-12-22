using Carter;
using MediatR;
using Services.ProductService.ProductCategory.API.Models.DTOs;

namespace Services.ProductService.ProductCategory.API.Categories.CreateCategory;

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
        }).RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("CreateCategory")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Category")
        .WithDescription("Serve for creating category object");
    }
}
