using BasketECommerce.Web.Models.Orders;
using BasketECommerce.Web.Models.ProductCategory;
using BasketECommerce.Web.Models.ShoppingCart;
using BasketECommerce.Web.Services.Ordering;
using BasketECommerce.Web.Services.ProductCategory;
using BasketECommerce.Web.Services.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace BasketECommerce.Web.Controllers;
public class CartController : Controller
{
    private readonly IShoppingCartService _shoppingCartService;

    private readonly IProductService _productService;

    private readonly IOrderingService _orderingService;

    public CartController(IShoppingCartService shoppingCartService, IProductService productService, IOrderingService orderingService)
    {
        _shoppingCartService = shoppingCartService;
        _productService = productService;
        _orderingService = orderingService;
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
            OrderDetails = await PopulateOrderDetails()
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

    #region HelperMethods

    private async Task<ShoppingCartModel> LoadCartBasedOnLoggedUser()
    {
        var userId = User.Claims.Where(l => l.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value;

        var productServiceApiResponse = await _productService.GetAllProducts();
        var shoppingCartServiceApiResponse = await _shoppingCartService.GetShoppingCartByUserId(userId);

        if (!productServiceApiResponse.IsSuccessStatusCode || !shoppingCartServiceApiResponse.IsSuccessStatusCode)
            return null;

        var productList = JsonConvert.DeserializeObject<List<ProductModel>>(Convert.ToString(productServiceApiResponse.Content.Result));

        var shoppingCart = JsonConvert.DeserializeObject<ShoppingCartModel>(Convert.ToString(shoppingCartServiceApiResponse.Content.Result));

        foreach(var item in shoppingCart.CartItems)
        {
            var productModel = productList.FirstOrDefault(l => l.Id == item.Id);

            if (productModel != null)
            {
                item.ProductModel = productModel;
            }
        }
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
                ProductName = product.ProductModel.Name,
                Quantity = product.Quantity,
                Price = product.Price,
            };

            orderDetails.Add(orderDetail);
        }

        return orderDetails;
    }

    #endregion
}
