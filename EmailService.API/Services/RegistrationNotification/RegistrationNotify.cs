using EmailService.API.FactoryAppDbContext;
using EmailService.API.Models;
using EmailService.API.Models.DTOs;
using Mapster;

namespace EmailService.API.Services.RegistrationNotification;

public class RegistrationNotify : IRegistrationNotify
{
    private readonly IAppDbContextFactory _factory;
    public RegistrationNotify(IAppDbContextFactory factory)
    {
        _factory = factory;
    }
    public async Task NewRegistrationNotification(RegistrationNotifyDTO registrationNotify)
    {
        using var context = _factory.Create();

        var emailLoggerDto = new EmailLoggerDTO
        {
            Message = $"New user registered. Email: {registrationNotify.Email}, Name: {registrationNotify.Name} ",
            Discriminator = "AuthenticationService"
        };

        var emailLogger = emailLoggerDto.Adapt<EmailLogger>();

        await context.EmailLoggers.AddAsync(emailLogger);
        await context.SaveChangesAsync();
    }
}
