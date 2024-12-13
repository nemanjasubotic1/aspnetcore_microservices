using BasketECommerce.Web.Models;
using Refit;
using static BasketECommerce.Web.Models.ProductCategory.ProductModel;

namespace BasketECommerce.Web.Services.ProductCategory;

public interface IProductService
{

    [Get("/product?pageNumber={PageNumber}&pageSize={PageSize}")]
    Task<ApiResponse<CustomApiResponse>> GetAllProducts(int? PageNumber = 1, int? PageSize = 10);

    [Get("/product/{id}")]
    Task<ApiResponse<CustomApiResponse>> GetProductById(Guid? Id);

    [Post("/product")]
    Task<ApiResponse<CustomApiResponse>> CreateProduct(CreateProductRequest request);

    [Put("/product")]
    Task<ApiResponse<CustomApiResponse>> UpdateProduct(UpdateProductRequest request);

    [Delete("/product/{id}")]
    Task<ApiResponse<CustomApiResponse>> DeleteProduct(Guid? Id);
}
