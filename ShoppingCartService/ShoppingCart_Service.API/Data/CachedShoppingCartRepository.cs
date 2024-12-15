using GeneralUsing.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using ShoppingCart_Service.API.Models;

namespace ShoppingCart_Service.API.Data;

public class CachedShoppingCartRepository : IShoppingCartRepository
{
    private readonly IShoppingCartRepository _repository;
    private readonly IDistributedCache _cache;
    public CachedShoppingCartRepository(IShoppingCartRepository repository, IDistributedCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<ShoppingCart> CreateShoppingCart(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        await _repository.CreateShoppingCart(shoppingCart, cancellationToken);

        await _cache.SetRecordAsync<ShoppingCart>(shoppingCart.Id.ToString(), shoppingCart, null, cancellationToken);

        return shoppingCart;

    }

    public async Task<ShoppingCart> GetShoppingCartByIdAsync(Guid cartId, CancellationToken cancellationToken = default)
    {
        var cachedCart = await _cache.GetRecordAsync<ShoppingCart>(cartId.ToString(), cancellationToken);

        if (cachedCart != null)
        {
            return cachedCart;
        }

        var shoppingCart = await _repository.GetShoppingCartByIdAsync(cartId, cancellationToken);

        await _cache.SetRecordAsync<ShoppingCart>(shoppingCart.Id.ToString(), shoppingCart, null, cancellationToken);

        return shoppingCart;
    }

    public async Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var shoppingCart = await _repository.GetShoppingCartByUserIdAsync(userId, cancellationToken);

        return shoppingCart;
    }

    public Task<bool> RemoveItemFromShoppingCart(Guid itemId, string userId, CancellationToken cancellationToken = default)
    {
        return _repository.RemoveItemFromShoppingCart(itemId, userId, cancellationToken);
    }

    public async Task<bool> RemoveShoppingCart(Guid cartId, CancellationToken cancellationToken = default)
    {
        var isDeleted = await _repository.RemoveShoppingCart(cartId, cancellationToken);

        if (isDeleted)
        {
            await _cache.RemoveAsync(cartId.ToString(), cancellationToken);
            return true;
        }

        return false;
    }

    public async Task UpdateShoppingCart(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        await _repository.UpdateShoppingCart(shoppingCart, cancellationToken);
    }
}
