using EmailService.API.Models.DTOs;

namespace EmailService.API.Services.ShoppingCartEmail;

public interface IShoppingCartEmailService
{
    Task SendAndStoreCart(ShoppingCartDTO shoppingCartDTO);
}
