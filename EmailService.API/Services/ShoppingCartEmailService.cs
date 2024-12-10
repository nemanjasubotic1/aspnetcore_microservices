
using EmailService.API.FactoryAppDbContext;
using EmailService.API.Models;
using EmailService.API.Models.DTOs;
using Mapster;

namespace EmailService.API.Services;

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
