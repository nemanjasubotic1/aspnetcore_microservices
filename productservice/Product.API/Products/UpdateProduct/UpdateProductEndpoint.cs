﻿using Carter;
using FluentValidation;
using GeneralUsing.CQRS;
using Mapster;
using MediatR;
using ProductCategory.API.Models.DTOs;

namespace ProductCategory.API.Categories.UpdateCategory;

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

        });
    }
}
