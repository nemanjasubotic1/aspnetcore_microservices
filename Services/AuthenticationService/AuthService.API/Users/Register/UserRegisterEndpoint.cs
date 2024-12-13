using AuthService.API.Models.DTOs;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Users.Register;

public record UserRegisterRequest(RegistrationRequestDTO RegistrationRequestDTO);


public class UserRegisterEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/userauth/register", async ([FromBody] UserRegisterRequest request, ISender sender) =>
        {
            var command = new UserRegisterCommand(request.RegistrationRequestDTO);

            var result = await sender.Send(command);

            if (result.Errors != null && result.Errors.Count > 0)
            {
                return Results.BadRequest(result.Errors.Select(l => l.ErrorMessage));   
            }

            return Results.Created($"/userauth/register", result);

        });
    }
}
