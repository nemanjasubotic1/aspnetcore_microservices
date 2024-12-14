using BasketECommerce.Web.Models;
using BasketECommerce.Web.Models.ProductCategory;
using BasketECommerce.Web.Models.ShoppingCart;
using BasketECommerce.Web.Services.ProductCategory;
using BasketECommerce.Web.Services.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;

namespace BasketECommerce.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    private readonly IProductService _productService;

    private readonly IShoppingCartService _shoppingCartService;

    public HomeController(ILogger<HomeController> logger, IProductService productService, IShoppingCartService shoppingCartService)
    {
        _logger = logger;
        _productService = productService;
        _shoppingCartService = shoppingCartService;
    }

    public async Task<IActionResult> Index()
    {
        var apiResponse = await _productService.GetAllProducts();

        if (!apiResponse.IsSuccessStatusCode)
            return BadRequest(apiResponse);

        var categoryResponse = apiResponse.Content;

        var jsonResponseList = JsonConvert.DeserializeObject<List<ProductModel>>(Convert.ToString(categoryResponse.Result));

        return View(jsonResponseList);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid? productId)
    {
        var apiResponse = await _productService.GetProductById(productId);

        if (!apiResponse.IsSuccessStatusCode && apiResponse.Content == null)
        {
            var errorResponse = apiResponse.Error.Content;

            TempData["error"] = "Error occured.";

            return RedirectToAction(nameof(Index));
        }

        var product = JsonConvert.DeserializeObject<ProductModel>(Convert.ToString(apiResponse.Content.Result));

        CartItemModel cartItemModel = new()
        {
            Id = product.Id, 
            ProductModel = product,
            ProductName = product.Name,
            Price = product.Price,
            Quantity=  0
        };


        return View(cartItemModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Details(CartItemModel cartItemModel)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity!;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        ShoppingCartModel model = new()
        {
            Discount = 0,
            UserId = claim.Value,
            CartItems =
            {
                cartItemModel
            }
        };

        var shoppingCartRequest = new CreateShoppingCartRequest(model);

        var apiResponse = await _shoppingCartService.CreateShoppingCart(shoppingCartRequest);

        if (!apiResponse.IsSuccessStatusCode && apiResponse.Content == null)
        {
            var errorResponse = apiResponse.Error.Content;

            TempData["error"] = "Error occured.";

            return RedirectToAction(nameof(Index));
        }

        int numberOfShoppingCartItems = await SD.GetNumberOfCartItems(_shoppingCartService, claim);

        HttpContext.Session.SetInt32(SD.SessionCart, numberOfShoppingCartItems);

        return RedirectToAction(nameof(Index));
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
