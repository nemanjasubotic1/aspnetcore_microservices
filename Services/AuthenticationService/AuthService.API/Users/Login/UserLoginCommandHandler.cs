using AuthService.API.Models.DTOs;
using AuthService.API.Services.IServices;
using FluentValidation;
using FluentValidation.Results;
using GeneralUsing.CQRS;

namespace AuthService.API.Users.Login;

public record UserLoginCommand(LoginRequestDTO LoginRequestDTO) : ICommand<CustomApiResponse>;

public class UserLoginCommandCommandValidator : AbstractValidator<UserLoginCommand>
{
    public UserLoginCommandCommandValidator()
    {
        RuleFor(l => l.LoginRequestDTO.Username).NotEmpty().WithMessage("Username is required");
        RuleFor(l => l.LoginRequestDTO.Password).NotEmpty().WithMessage("Password is required");
    }
}

public class UserLoginCommandHandler(IAuthenticationService _authenticationService) : ICommandHandler<UserLoginCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var loginResponseDto = await _authenticationService.Login(request.LoginRequestDTO);

        if (loginResponseDto.UserDTO ==  null)
        {
            return new CustomApiResponse(null, false, [new ValidationFailure("", "Login failed")]);
        }

        return new CustomApiResponse(loginResponseDto);

    }
}


