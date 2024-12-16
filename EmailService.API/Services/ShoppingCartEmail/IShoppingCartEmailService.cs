using Services.EmailService.EmailService.API.Models.DTOs;

namespace Services.EmailService.EmailService.API.Services.ShoppingCartEmail;

public interface IShoppingCartEmailService
{
    Task SendAndStoreCart(ShoppingCartDTO shoppingCartDTO);
}
