using Carter;
using FluentValidation;
using GeneralUsing.CQRS;
using MediatR;

namespace Main.ProductService.ProductCategory.API.InitialData;

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

        }).RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("UpdateCategory")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("UpdateCategory")
        .WithDescription("Serve for updating single category");
    }
}
