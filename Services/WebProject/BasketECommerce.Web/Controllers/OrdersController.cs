using BasketECommerce.Web.Models.Orders;
using BasketECommerce.Web.Services.Ordering;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BasketECommerce.Web.Controllers;
public class OrdersController : Controller
{
    private readonly IOrderingService _orderingService;
    public OrdersController(IOrderingService orderingService)
    {
        _orderingService = orderingService;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> OrderDetails(string? orderId)
    {
        var apiResponse = await _orderingService.GetOrderById(new Guid(orderId));

        if (!apiResponse.IsSuccessStatusCode)
        {
            var errorResponse = apiResponse.Error.Content;

            TempData["error"] = errorResponse ?? "Error occured.";

            return RedirectToAction(nameof(Index));
        }

        var orderHeaderDTO = JsonConvert.DeserializeObject<OrderHeaderDTO>(Convert.ToString(apiResponse.Content.Result));

        return View(orderHeaderDTO);
    }

    [HttpPost]
    [ActionName(nameof(OrderDetails))]
    public async Task<IActionResult> OrderDetailsPOST(string? orderId)
    {
        var apiResponse = await _orderingService.ChangeOrderStatus(new ChangeOrderStatusRequest(new Guid(orderId), OrderStatus.Verified.ToString()));

        if (!apiResponse.IsSuccessStatusCode)
        {
            var errorResponse = apiResponse.Error.Content;

            //TempData["error"] = errorResponse ?? "Error occured.";

            return BadRequest(errorResponse ?? "Error occured.");
        }

        return RedirectToAction(nameof(Index));
    }


    #region APICALLS

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var apiResponse = await _orderingService.GetAllOrders();

        if (!apiResponse.IsSuccessStatusCode)
            return BadRequest(apiResponse);

        var ordesResponse = apiResponse.Content;

        var jsonResponseList = JsonConvert.DeserializeObject<List<OrderHeaderDTO>>(Convert.ToString(ordesResponse.Result));

        return Json(new { orders = jsonResponseList });
    }

    #endregion
}
