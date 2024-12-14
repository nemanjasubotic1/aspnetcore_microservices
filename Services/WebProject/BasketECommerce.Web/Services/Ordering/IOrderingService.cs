using BasketECommerce.Web.Models;
using BasketECommerce.Web.Models.Orders;
using Refit;

namespace BasketECommerce.Web.Services.Ordering;

public interface IOrderingService
{
    [Post("/order")]
    Task<ApiResponse<CustomApiResponse>> CreateOrder(CreateOrderRequest request);
}
