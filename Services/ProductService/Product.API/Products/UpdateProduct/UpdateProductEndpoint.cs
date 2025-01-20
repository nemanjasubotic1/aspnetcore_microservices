using Carter;
using FluentValidation;
using MediatR;

namespace Main.ProductService.ProductCategory.API.InitialData;

public record UpdateProductRequest(ProductDTO ProductDTO);
//public record UpdateProductResponse(Guid Id);


public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/product", async (UpdateProductRequest request, ISender sender) =>
        {
            var command = new UpdateProductCommand(request.ProductDTO);

            var result = await sender.Send(command);

            //var response = result.Adapt<UpdateProductResponse>();

            return Results.Ok(result);

        }).RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("UpdateProduct")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("UpdateProduct")
        .WithDescription("Serve for updating single product");
    }
}
