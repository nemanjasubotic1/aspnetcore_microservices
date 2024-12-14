using BasketECommerce.Web.Models;
using BasketECommerce.Web.Models.ShoppingCart;
using Refit;

namespace BasketECommerce.Web.Services.ShoppingCart;

public interface IShoppingCartService
{
    [Post("/shoppingCart")]
    Task<ApiResponse<CustomApiResponse>> CreateShoppingCart(CreateShoppingCartRequest request);

    [Get("/shoppingCart/usercart/{Id}")]
    Task<ApiResponse<CustomApiResponse>> GetShoppingCartByUserId(string Id);

    [Delete("/shoppingcart/itemremove")]
    Task<ApiResponse<CustomApiResponse>> RemoveItemFromShoppingCart([Body] RemoveItemFromCartByItemIdRequest request);

    [Delete("/shoppingcart/{id}")]
    Task<ApiResponse<CustomApiResponse>> DeleteShoppingCart(Guid Id);
}
