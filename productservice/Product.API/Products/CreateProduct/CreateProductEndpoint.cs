using Carter;
using Mapster;
using MediatR;
using ProductCategory.API.Models.DTOs;

namespace ProductCategory.API.Products.CreateProduct;

public record CreateProductRequest(ProductDTO ProductDTO);
//public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/product", async (CreateProductRequest? request, ISender sender) =>
        {
            var command = new CreateProductCommand(request.ProductDTO);

            var result = await sender.Send(command);

            if (result.Errors != null && result.Errors.Any())
            {
                return Results.BadRequest(result.Errors.Select(l => l.ErrorMessage));
            }

            return Results.Created($"/product", result);

        });
    }
}
