using Carter;
using Mapster;
using MediatR;
using ProductCategory.API.Models.DTOs;

namespace ProductCategory.API.Products.CreateProduct;

public record CreateProductRequest(ProductDTO ProductDTO);
public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/product", async (CreateProductRequest request, ISender sender) =>
        {

            var command = new CreateProductCommand(request.ProductDTO);

            var result = await sender.Send(command);

            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/product/{response.Id}", response);

        });
    }
}
