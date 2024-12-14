using BasketECommerce.Web.Services.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BasketECommerce.Web.ViewComponents;

public class ShoppingCartViewComponent : ViewComponent
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartViewComponent(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity!;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        if (claim != null)
        {
            if (HttpContext.Session.GetInt32(SD.SessionCart) == null)
            {

                int numberOfShoppingCartItems = await SD.GetNumberOfCartItems(_shoppingCartService, claim);

                HttpContext.Session.SetInt32(SD.SessionCart, numberOfShoppingCartItems);
            }

            return View(HttpContext.Session.GetInt32(SD.SessionCart));
        }
        else
        {
            HttpContext.Session.Clear();
            return View(0);
        }

    }
}
