using ShoppingCart_Service.API.Models;
using System.Runtime.CompilerServices;

namespace ShoppingCart_Service.API.Data;

public interface IShoppingCartRepository
{
    Task<ShoppingCart> GetShoppingCartByIdAsync(Guid cartId, CancellationToken cancellationToken = default);
    Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<ShoppingCart> CreateShoppingCart(ShoppingCart shoppingCart, CancellationToken cancellationToken = default);
    Task<bool> RemoveShoppingCart(Guid cartId, CancellationToken cancellationToken = default);
    Task<bool> RemoveItemFromShoppingCart(Guid itemId, string userId, CancellationToken cancellationToken = default);
    Task UpdateShoppingCart(ShoppingCart shoppingCart, CancellationToken cancellationToken = default);
}
