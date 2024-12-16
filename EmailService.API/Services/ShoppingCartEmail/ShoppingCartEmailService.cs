using Mapster;
using Services.EmailService.EmailService.API.FactoryAppDbContext;
using Services.EmailService.EmailService.API.Models;
using Services.EmailService.EmailService.API.Models.DTOs;

namespace Services.EmailService.EmailService.API.Services.ShoppingCartEmail;

public class ShoppingCartEmailService : IShoppingCartEmailService
{
    private readonly IAppDbContextFactory _factory;
    public ShoppingCartEmailService(IAppDbContextFactory factory)
    {
        _factory = factory;
    }
    public async Task SendAndStoreCart(ShoppingCartDTO shoppingCartDTO)
    {
        // SEND EMAIL SERVICE -- TODO

        // STORE DATA IN DB

        using var context = _factory.Create();

        var emailLoggerDto = new EmailLoggerDTO
        {
            Message = $"Shopping cart successfully emailed. The cart is created {shoppingCartDTO.CreatedAt}",
            Discriminator = "ShoppingCart"
        };

        var emailLogger = emailLoggerDto.Adapt<EmailLogger>();

        await context.EmailLoggers.AddAsync(emailLogger);
        await context.SaveChangesAsync();
    }
}
