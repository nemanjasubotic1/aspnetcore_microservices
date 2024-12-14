using EmailService.API.Models.DTOs;

namespace EmailService.API.Services.RegistrationNotification;

public interface IRegistrationNotify
{
    Task NewRegistrationNotification(RegistrationNotifyDTO registrationNotify);
}
