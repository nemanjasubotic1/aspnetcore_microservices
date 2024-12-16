using Services.EmailService.EmailService.API.Models.DTOs;

namespace Services.EmailService.EmailService.API.Services.RegistrationNotification;

public interface IRegistrationNotify
{
    Task NewRegistrationNotification(RegistrationNotifyDTO registrationNotify);
}
