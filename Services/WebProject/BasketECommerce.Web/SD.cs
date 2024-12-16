using BasketECommerce.Web.Models.ShoppingCart;
using BasketECommerce.Web.Services.ShoppingCart;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BasketECommerce.Web;

public static class SD
{
    public static string SessionCart = "SessionShoppingCart";

    public const string Admin_Role = "Admin";
    public const string Customer_Role = "Customer";

    public enum OrderStatus
    {
        Draft, Pending, Completed, Canceled
    }


    public static async Task<int> GetNumberOfCartItems(IShoppingCartService _shoppingCartService, Claim claim)
    {
        var userId = claim.Value;

        var apiResponse = await _shoppingCartService.GetShoppingCartByUserId(userId);

        if (!apiResponse.IsSuccessStatusCode)
        {
            return 0;
        }

        var content = apiResponse.Content;

        var shoppingCartModel = JsonConvert.DeserializeObject<ShoppingCartModel>(Convert.ToString(apiResponse.Content.Result));

        int numberOfShoppingCartItems = shoppingCartModel.CartItems.Count;

        return numberOfShoppingCartItems;
    }
}
