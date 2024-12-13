using BasketECommerce.Web.Models.ProductCategory;
using BasketECommerce.Web.Services.ProductCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace BasketECommerce.Web.Controllers;

[Authorize]
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
    public IActionResult CreateCategory()
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

        if (categoryResponse == null || !categoryResponse.IsSuccess)
        {
            TempData["error"] = "Error occured.";

            return RedirectToAction(nameof(Index));
        }

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
    public async Task<IActionResult> DeleteCategory(Guid? categoryId)
    {
        var apiResponse = await _categoryService.GetCategoryById(categoryId);

        if (!apiResponse.IsSuccessStatusCode)
            return BadRequest(apiResponse);

        var categoryResponse = apiResponse.Content;

        if (categoryResponse == null || !categoryResponse.IsSuccess)
        {
            TempData["error"] = "Error occured.";

            return RedirectToAction(nameof(Index));
        }

        var jsonResponseList = JsonConvert.DeserializeObject<CategoryModel>(Convert.ToString(categoryResponse.Result));

        return View(jsonResponseList);
    }

    [HttpPost]
    [ActionName(nameof(DeleteCategory))]
    public async Task<IActionResult> DeleteCategoryPOST(Guid? categoryId)
    {
        var apiResponse = await _categoryService.DeleteCategory(categoryId);

        if (!apiResponse.IsSuccessStatusCode)
            return BadRequest(apiResponse);

        return RedirectToAction(nameof(Index));
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
