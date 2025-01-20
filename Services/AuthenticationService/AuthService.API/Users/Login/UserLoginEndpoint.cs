using AuthService.API.Models.DTOs;
using AuthService.API.Users.Register;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Users.Login;

public record UserLoginRequest(LoginRequestDTO LoginRequestDTO);



public class UserLoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/userauth/login", async ([FromBody] UserLoginRequest request, ISender sender) =>
        {
            var command = new UserLoginCommand(request.LoginRequestDTO);

            var result = await sender.Send(command);

            if (result.Errors != null && result.Errors.Count > 0)
            {
                return Results.BadRequest(result.Errors.Select(l => l.ErrorMessage));
            }

            return Results.Created($"/userauth/login", result);
        });
    }
}
