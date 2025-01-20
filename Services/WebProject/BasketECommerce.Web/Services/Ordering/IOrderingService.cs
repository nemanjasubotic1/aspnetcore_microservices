using BasketECommerce.Web.Models;
using BasketECommerce.Web.Models.Orders;
using Refit;

namespace BasketECommerce.Web.Services.Ordering;

public interface IOrderingService
{
    [Post("/order")]
    Task<ApiResponse<CustomApiResponse>> CreateOrder(CreateOrderRequest request);

    [Put("/order/status")]
    Task<ApiResponse<CustomApiResponse>> ChangeOrderStatus(ChangeOrderStatusRequest request);

    [Get("/order/{id}")]
    Task<ApiResponse<CustomApiResponse>> GetOrdersByUserId(Guid Id);

    [Get("/order/singleorder/{id}")]
    Task<ApiResponse<CustomApiResponse>> GetOrderById(Guid Id);


    [Get("/order")]
    Task<ApiResponse<CustomApiResponse>> GetAllOrders([Body] GetAllOrdersQuery request);

}
