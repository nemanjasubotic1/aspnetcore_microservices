using BasketECommerce.Web.Models;
using BasketECommerce.Web.Models.ProductCategory;
using Refit;
using System.Runtime.CompilerServices;

namespace BasketECommerce.Web.Services.ProductCategory;

public interface ICategoryService
{

    [Get("/category?pageNumber={PageNumber}&pageSize={PageSize}")]
    Task<ApiResponse<CustomApiResponse>> GetAllCategories(int? PageNumber = 1, int? PageSize = 10);


    [Post("/category")]
    Task<ApiResponse<CustomApiResponse>> CreateCategory(CreateCategoryRequest request);

}
