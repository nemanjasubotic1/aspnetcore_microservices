using BasketECommerce.Web.Models.Orders;
using BasketECommerce.Web.Models.ShoppingCart;
using BasketECommerce.Web.Services.CouponService;
using BasketECommerce.Web.Services.Ordering;
using BasketECommerce.Web.Services.ProductCategory;
using BasketECommerce.Web.Services.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BasketECommerce.Web.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly IShoppingCartService _shoppingCartService;

    private readonly IProductService _productService;

    private readonly IOrderingService _orderingService;

    private readonly IDiscountService _discountService;

    public CartController(IShoppingCartService shoppingCartService, IProductService productService, IOrderingService orderingService, IDiscountService discountService)
    {
        _shoppingCartService = shoppingCartService;
        _productService = productService;
        _orderingService = orderingService;
        _discountService = discountService;
    }
    public async Task<IActionResult> Index()
    {
        var cart = await LoadCartBasedOnLoggedUser();

        return View(cart);
    }


    [HttpDelete]
    public async Task<IActionResult> Remove([FromBody] string Id)
    {

        var claimsIdentity = (ClaimsIdentity)User.Identity!;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        var apiResponse = await _shoppingCartService.RemoveItemFromShoppingCart(new RemoveItemFromCartByItemIdRequest(new Guid(Id), claim.Value));

        if (!apiResponse.IsSuccessStatusCode)
        {
            var errorResponse = apiResponse.Error.Content;

            TempData["error"] = "Error occured.";

            return NotFound();
        }

        int numberOfShoppingCartItems = await SD.GetNumberOfCartItems(_shoppingCartService, claim);

        HttpContext.Session.SetInt32(SD.SessionCart, numberOfShoppingCartItems);

        return Json(new { success = true });
    }


    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        var shoppingCartModel = await LoadCartBasedOnLoggedUser();
        var orderDetails = new List<OrderDetailsDTO>();

        OrderHeaderDTO orderHeaderDTO = new()
        {
            ShoppingCartId = shoppingCartModel.Id,
            TotalPrice = shoppingCartModel.CartTotal,
            OrderDetails = await PopulateOrderDetails(),
            Discount = shoppingCartModel.Discount,
        };


        return View(orderHeaderDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(OrderHeaderDTO orderHeaderDTO)
    {
        var userId = User.Claims.Where(l => l.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value;
        var emailAddress = User.Claims.Where(l => l.Type == ClaimTypes.Email)?.FirstOrDefault()?.Value;
        var name = User.Claims.Where(l => l.Type == ClaimTypes.Name)?.FirstOrDefault()?.Value;

        orderHeaderDTO.OrderDetails = await PopulateOrderDetails();

        CustomerDTO customerDTO = new()
        {
            Id = new Guid(userId),
            Username = emailAddress,
            Name = name,
            EmailAddress = emailAddress
        };

        CreateOrderRequest request = new CreateOrderRequest(orderHeaderDTO, customerDTO);

        var apiResponse = await _orderingService.CreateOrder(request);

        if (!apiResponse.IsSuccessStatusCode)
        {
            var errorResponse = apiResponse.Error.Content;

            TempData["error"] = "Error occured.";

            return View(orderHeaderDTO);
        }

        await _shoppingCartService.DeleteShoppingCart(orderHeaderDTO.ShoppingCartId);

        HttpContext.Session.SetInt32(SD.SessionCart, 0);

        return RedirectToAction(nameof(Confirmation));
    }

    [HttpGet]
    public IActionResult Confirmation()
    {
        return View();
    }

    #region APPLY_COUPON

    [HttpPost]
    public async Task<IActionResult> ApplyCoupon(ShoppingCartModel shoppingCartModel)
    {
        if (string.IsNullOrEmpty(shoppingCartModel.CouponName))
        {
            TempData["error"] = "Enter coupon";
            return RedirectToAction(nameof(Index));
        }

        var couponAvailable = await _discountService.GetCoupon(shoppingCartModel.CouponName);

        if (string.IsNullOrEmpty(couponAvailable.CouponName))
        {
            TempData["error"] = "Entered coupon dont exist.";
            return RedirectToAction(nameof(Index));
        }

        var applyRemoveDiscountDTO = new ApplyRemoveDiscountDTO()
        {
            Id = shoppingCartModel.Id,
            Discount = couponAvailable.Amount,
            CouponName = couponAvailable.CouponName,
        };

        var applyCouponRequest = new ApplyRemoveDiscountRequest(applyRemoveDiscountDTO);

        var apiResponse = await _shoppingCartService.ApplyRemoveDiscount(applyCouponRequest);

        if (!apiResponse.IsSuccessStatusCode && apiResponse.Content == null)
        {
            var errorResponse = apiResponse.Error.Content;

            TempData["error"] = "Error occured.";

            return RedirectToAction(nameof(Index));
        }

        TempData["success"] = $"Successfully submited coupon, discount of {couponAvailable.Amount} will be applied";

        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public async Task<IActionResult> RemoveCoupon(ShoppingCartModel shoppingCartModel)
    {
        var applyRemoveDiscountDTO = new ApplyRemoveDiscountDTO()
        {
            Id = shoppingCartModel.Id,
            Discount = 0,
            CouponName = "",
        };

        var applyCouponRequest = new ApplyRemoveDiscountRequest(applyRemoveDiscountDTO);

        var apiResponse = await _shoppingCartService.ApplyRemoveDiscount(applyCouponRequest);

        if (!apiResponse.IsSuccessStatusCode && apiResponse.Content == null)
        {
            var errorResponse = apiResponse.Error.Content;

            TempData["error"] = "Error occured.";

            return RedirectToAction(nameof(Index));
        }

        TempData["success"] = $"The coupon {shoppingCartModel.CouponName} is removed.";

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region HelperMethods

    private async Task<ShoppingCartModel> LoadCartBasedOnLoggedUser()
    {
        var userId = User.Claims.Where(l => l.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value;

        var shoppingCartServiceApiResponse = await _shoppingCartService.GetShoppingCartByUserId(userId);

        if (!shoppingCartServiceApiResponse.IsSuccessStatusCode)
            return null;

        var shoppingCart = JsonConvert.DeserializeObject<ShoppingCartModel>(Convert.ToString(shoppingCartServiceApiResponse.Content.Result));

        return shoppingCart;
    }

    private async Task<List<OrderDetailsDTO>> PopulateOrderDetails()
    {
        var shoppingCartModel = await LoadCartBasedOnLoggedUser();
        var orderDetails = new List<OrderDetailsDTO>();

        foreach (var product in shoppingCartModel.CartItems)
        {
            var orderDetail = new OrderDetailsDTO
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                Quantity = product.Quantity,
                Price = product.Price,
            };

            orderDetails.Add(orderDetail);
        }

        return orderDetails;
    }

    #endregion
}
