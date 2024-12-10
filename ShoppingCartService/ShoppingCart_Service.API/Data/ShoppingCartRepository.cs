﻿using Marten;
using ShoppingCart_Service.API.Models;

namespace ShoppingCart_Service.API.Data;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly IDocumentSession _session;
    public ShoppingCartRepository(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<ShoppingCart> CreateShoppingCart(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        _session.Store<ShoppingCart>(shoppingCart);
        await _session.SaveChangesAsync(cancellationToken);

        return shoppingCart;
    }

    public async Task<ShoppingCart> GetShoppingCartByIdAsync(Guid cartId, CancellationToken cancellationToken = default)
    {
        var shoppingCart = await _session.LoadAsync<ShoppingCart>(cartId, cancellationToken);

        if (shoppingCart == null)
        {
            return new ShoppingCart();
        }

        return shoppingCart;
    }

    public async Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var shoppingCart = await _session.Query<ShoppingCart>().Where(l => l.UserId == userId).FirstOrDefaultAsync();

        if (shoppingCart == null)
        {
            return new ShoppingCart();
        }

        return shoppingCart;
    }

    public async Task<bool> RemoveShoppingCart(Guid cartId, CancellationToken cancellationToken = default)
    {
        var cart = await GetShoppingCartByIdAsync(cartId, cancellationToken);

        if (cart.UserId == null)
        {
            return false;
        }

        _session.Delete<ShoppingCart>(cartId);
        await _session.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task UpdateShoppingCart(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {

        _session.Update<ShoppingCart>(shoppingCart);
        await _session.SaveChangesAsync(cancellationToken);
    }
}
