using BasketECommerce.Web.Models;
using BasketECommerce.Web.Models.ProductCategory;
using BasketECommerce.Web.Services.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BasketECommerce.Web.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICategoryService _productCategoryService;
    public HomeController(ILogger<HomeController> logger, ICategoryService productCategoryService)
    {
        _logger = logger;
        _productCategoryService = productCategoryService;
    }

    public IActionResult Index()
    {
        return View();
    }
  

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
