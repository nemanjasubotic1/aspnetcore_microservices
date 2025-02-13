﻿using Carter;
using MediatR;

namespace Main.ProductService.ProductCategory.API.InitialData;

//public record DeleteCategoryRequest(Guid Id);
//public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/product/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(id));

            //var response = result.Adapt<DeleteProductResponse>();

            return Results.Ok(result);

        }).RequireAuthorization(policy => policy.RequireRole("Admin"))
        .WithName("DeleteProduct")
        .Produces<CustomApiResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("UpdateProduct")
        .WithDescription("Serve for deleting single product");
    }
}
