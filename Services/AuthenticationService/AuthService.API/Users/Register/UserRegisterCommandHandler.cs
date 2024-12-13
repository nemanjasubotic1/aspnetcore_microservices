using AuthService.API.Models.DTOs;
using AuthService.API.Services.IServices;
using FluentValidation;
using FluentValidation.Results;
using GeneralUsing.CQRS;

namespace AuthService.API.Users.Register;

public record UserRegisterCommand(RegistrationRequestDTO RegistrationRequestDTO) : ICommand<CustomApiResponse>;


public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
{
    public UserRegisterCommandValidator()
    {
        RuleFor(l => l.RegistrationRequestDTO.Name).NotEmpty().WithMessage("The name is required");
        RuleFor(l => l.RegistrationRequestDTO.Email).NotEmpty().WithMessage("The email is required");
        RuleFor(l => l.RegistrationRequestDTO.Password).NotEmpty().WithMessage("The password is required");
    }
}


public class UserRegisterCommandHandler(IAuthenticationService _authenticationService) : ICommandHandler<UserRegisterCommand, CustomApiResponse>
{

    public async Task<CustomApiResponse> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var errorMessage = await _authenticationService.Register(request.RegistrationRequestDTO);

        if (!string.IsNullOrEmpty(errorMessage))
        {
            return new CustomApiResponse(null, false, [new ValidationFailure("", errorMessage)]);
        }

        // TODO RabittMQ to send message with registration
        
        return new CustomApiResponse();
    }
}


