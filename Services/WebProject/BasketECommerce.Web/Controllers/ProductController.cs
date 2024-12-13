using BasketECommerce.Web.Models.ProductCategory;
using BasketECommerce.Web.Models.ProductCategory.ViewModels;
using BasketECommerce.Web.Services.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel;
using static BasketECommerce.Web.Models.ProductCategory.ProductModel;

namespace BasketECommerce.Web.Controllers;
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }
    public IActionResult Index()
    {
        return View();
    }


    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        ProductVM productVM = new();

        var categoryList = await CategoryList();

        if (categoryList == null)
        {
            TempData["error"] = "Error occured.";

            return RedirectToAction(nameof(Index));
        }

        IEnumerable<SelectListItem> Items = categoryList.Select(l => new SelectListItem
        {
            Value = l.Id.ToString(),
            Text = l.Name,
        });

        productVM.CategoryItems = Items;

        return View(productVM);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductVM productVM)
    {
        var productRequest = new CreateProductRequest(productVM.ProductModel);

        var apiResponse = await _productService.CreateProduct(productRequest);

        if (!apiResponse.IsSuccessStatusCode)
        {
            var errorResponse = apiResponse.Error.Content;

            ModelState.AddModelError("", errorResponse ?? "Error occured");

            var categoryList = await CategoryList();

            if (categoryList == null)
            {
                TempData["error"] = "Error occured.";

                return RedirectToAction(nameof(Index));
            }

            IEnumerable<SelectListItem> Items = categoryList.Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.Name,
            });

            productVM.CategoryItems = Items;

            return View(productVM);
        }

        return RedirectToAction(nameof(Index));
    }




    [HttpGet]
    public async Task<IActionResult> UpdateProduct(Guid? productId)
    {
        ProductVM productVM = new();

        var categoryList = await CategoryList();

        if (categoryList == null)
        {
            TempData["error"] = "Error occured.";

            return RedirectToAction(nameof(Index));
        }

        IEnumerable<SelectListItem> Items = categoryList.Select(l => new SelectListItem
        {
            Value = l.Id.ToString(),
            Text = l.Name,
        });

        productVM.CategoryItems = Items;

        var apiResponse = await _productService.GetProductById(productId);

        if (!apiResponse.IsSuccessStatusCode)
            return BadRequest(apiResponse);

        var productResponse = apiResponse.Content;

        if (productResponse == null || !productResponse.IsSuccess)
        {
            TempData["error"] = "Error occured.";

            return RedirectToAction(nameof(Index));
        }

        var jsonResponseList = JsonConvert.DeserializeObject<ProductModel>(Convert.ToString(productResponse.Result));

        productVM.ProductModel = jsonResponseList;

        return View(productVM);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(ProductVM productVM)
    {
        var productRequest = new UpdateProductRequest(productVM.ProductModel);

        var apiResponse = await _productService.UpdateProduct(productRequest);

        if (!apiResponse.IsSuccessStatusCode)
        {
            var errorResponse = apiResponse.Error.Content;

            ModelState.AddModelError("", errorResponse ?? "Error occured");

            var categoryList = await CategoryList();

            if (categoryList == null)
            {
                TempData["error"] = "Error occured.";

                return RedirectToAction(nameof(Index));
            }

            IEnumerable<SelectListItem> Items = categoryList.Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.Name,
            });

            productVM.CategoryItems = Items;

            return View(productVM);
        }

        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> DeleteProduct(Guid? productId)
    {
        ProductVM productVM = new();

        var categoryList = await CategoryList();

        if (categoryList == null)
        {
            TempData["error"] = "Error occured.";

            return RedirectToAction(nameof(Index));
        }

        IEnumerable<SelectListItem> Items = categoryList.Select(l => new SelectListItem
        {
            Value = l.Id.ToString(),
            Text = l.Name,
        });

        productVM.CategoryItems = Items;

        var apiResponse = await _productService.GetProductById(productId);

        if (!apiResponse.IsSuccessStatusCode)
            return BadRequest(apiResponse);

        var productResponse = apiResponse.Content;

        if (productResponse == null || !productResponse.IsSuccess)
        {
            TempData["error"] = "Error occured.";

            return RedirectToAction(nameof(Index));
        }

        var jsonResponseList = JsonConvert.DeserializeObject<ProductModel>(Convert.ToString(productResponse.Result));

        productVM.ProductModel = jsonResponseList;

        return View(productVM);
    }

    [HttpPost]
    [ActionName(nameof(DeleteProduct))]
    public async Task<IActionResult> DeleteProductPOST(Guid? productId)
    {
        var apiResponse = await _productService.DeleteProduct(productId);

        if (!apiResponse.IsSuccessStatusCode)
            return BadRequest(apiResponse);

        return RedirectToAction(nameof(Index));
    }


    #region APICALLS

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var apiResponse = await _productService.GetAllProducts();

        if (!apiResponse.IsSuccessStatusCode)
            return BadRequest(apiResponse);

        var categoryResponse = apiResponse.Content;

        var jsonResponseList = JsonConvert.DeserializeObject<List<ProductModel>>(Convert.ToString(categoryResponse.Result));

        return Json(new { products = jsonResponseList });
    }

    #endregion

    #region HELPER METHODS

    private async Task<List<CategoryModel>> CategoryList()
    {
        var apiResponse = await _categoryService.GetAllCategories();

        if (!apiResponse.IsSuccessStatusCode)
            return null;

        var categoryResponse = apiResponse.Content;

        var jsonResponseList = JsonConvert.DeserializeObject<List<CategoryModel>>(Convert.ToString(categoryResponse.Result));

        return jsonResponseList;
    }

    #endregion

}
