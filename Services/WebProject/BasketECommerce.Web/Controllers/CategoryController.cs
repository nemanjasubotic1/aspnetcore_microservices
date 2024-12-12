using BasketECommerce.Web.Models.ProductCategory;
using BasketECommerce.Web.Services.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace BasketECommerce.Web.Controllers;
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService productCategoryService)
    {
        _categoryService = productCategoryService;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> CreateCategory()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> CreateCategory(CategoryModel categoryModel)
    {
        var categoryRequest = new CreateCategoryRequest(categoryModel);

        var apiResponse = await _categoryService.CreateCategory(categoryRequest);

        if (!apiResponse.IsSuccessStatusCode)
        {
            var errorResponse = apiResponse.Error.Content;

            ModelState.AddModelError("", errorResponse ?? "Error occured");

            return View(categoryModel);
        }

        Console.WriteLine(apiResponse.Content);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> UpdateCategory(Guid? categoryId)
    {
        var apiResponse = await _categoryService.GetCategoryById(categoryId);

        if (!apiResponse.IsSuccessStatusCode)
            return BadRequest(apiResponse);

        var categoryResponse = apiResponse.Content;

        var jsonResponseList = JsonConvert.DeserializeObject<CategoryModel>(Convert.ToString(categoryResponse.Result));

        return View(jsonResponseList);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCategory(CategoryModel categoryModel)
    {
        var categoryRequest = new UpdateCategoryRequest(categoryModel);

        var apiResponse = await _categoryService.UpdateCategory(categoryRequest);

        if (!apiResponse.IsSuccessStatusCode)
        {
            var errorResponse = apiResponse.Error.Content;

            ModelState.AddModelError("", errorResponse ?? "Error occured");

            return View(categoryModel);
        }

        Console.WriteLine(apiResponse.Content);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> DeleteCategory(int? categoryId)
    {


        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCategory(CategoryModel categoryModel)
    {


        return View();
    }


    #region APICALLS

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var apiResponse = await _categoryService.GetAllCategories();

        if (!apiResponse.IsSuccessStatusCode)
            return BadRequest(apiResponse);

        var categoryResponse = apiResponse.Content;

        var jsonResponseList = JsonConvert.DeserializeObject<List<CategoryModel>>(Convert.ToString(categoryResponse.Result));

        return Json(new { categories = jsonResponseList });
    }

    #endregion
}
