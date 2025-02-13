﻿using AuthService.API.Models.DTOs;
using AuthService.API.Services.IServices;
using FluentValidation;
using FluentValidation.Results;
using GeneralUsing.CQRS;
using Integration.RabbitMQSender;

namespace AuthService.API.Users.Register;

public record UserRegisterCommand(RegistrationDTO RegistrationRequestDTO) : ICommand<CustomApiResponse>;


public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
{
    public UserRegisterCommandValidator()
    {
        RuleFor(l => l.RegistrationRequestDTO.Name).NotEmpty().WithMessage("The name is required");
        RuleFor(l => l.RegistrationRequestDTO.Email).NotEmpty().WithMessage("The email is required");
        RuleFor(l => l.RegistrationRequestDTO.Password).NotEmpty().WithMessage("The password is required");
    }
}


public class UserRegisterCommandHandler(IAuthenticationService _authenticationService, 
    IRabbitMQMessageSender messageSender) : ICommandHandler<UserRegisterCommand, CustomApiResponse>
{

    public async Task<CustomApiResponse> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var errorMessage = await _authenticationService.Register(request.RegistrationRequestDTO);

        if (!string.IsNullOrEmpty(errorMessage))
        {
            return new CustomApiResponse(null, false, [new ValidationFailure("", errorMessage)]);
        }

        // send message to rabbitmq
        var registrationData = new
        {
            request.RegistrationRequestDTO.Email,
            request.RegistrationRequestDTO.Name,
        };

        var queueName = "emailregistration";

        await messageSender.SendMessage(registrationData, queueName);

        return new CustomApiResponse();
    }
}


