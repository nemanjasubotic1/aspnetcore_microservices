﻿using Carter;
using Main.ShoppingCartService.ShoppingCart_Service.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart_Service.API.Models.DTOs;

namespace ShoppingCart_Service.API.ShoppingCarts.ApplyRemoveDiscount;

public record ApplyRemoveDiscountRequest(ApplyRemoveDiscountDTO ApplyRemoveDiscountDTO);

public class ApplyRemoveDiscountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/shoppingcart/discount", async ([FromBody] ApplyRemoveDiscountRequest request, ISender sender) =>
        {
            var command = new ApplyRemoveDiscountCommand(request.ApplyRemoveDiscountDTO);

            var result = await sender.Send(command);

            if (result.Errors != null && result.Errors.Any())
            {
                return Results.BadRequest(result.Errors.Select(l => l.ErrorMessage));
            }

            return Results.Created($"/product", result);

        }).RequireAuthorization()
        .WithName("ApplyRemoveDiscount")
        .Produces<CustomApiResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("ApplyRemoveDiscount")
        .WithDescription("Applying or removing discount coupon.");
    }
}
