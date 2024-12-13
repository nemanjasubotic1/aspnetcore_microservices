using BasketECommerce.Web.Models;
using BasketECommerce.Web.Models.ProductCategory;
using BasketECommerce.Web.Models.ShoppingCart;
using BasketECommerce.Web.Services.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Refit;
using System.Diagnostics;

namespace BasketECommerce.Web.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    public HomeController(ILogger<HomeController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
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
    public async Task<IActionResult> Details(CartItemModel cartItemModel)
    {
        


        return View(cartItemModel);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
