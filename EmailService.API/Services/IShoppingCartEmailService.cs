using EmailService.API.Models.DTOs;

namespace EmailService.API.Services;

public interface IShoppingCartEmailService
{
    Task SendAndStoreCart(ShoppingCartDTO shoppingCartDTO);
}
