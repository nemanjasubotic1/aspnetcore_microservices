using BasketECommerce.Web.Models.Authenication;
using BasketECommerce.Web.Models.Authentication;
using BasketECommerce.Web.Services.AuthenticationService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BasketECommerce.Web.Controllers;
public class AuthenticationController : Controller
{
    private readonly IAuthService _authService;

    private readonly ITokenProvider _tokenProvider;
    public AuthenticationController(IAuthService authService, ITokenProvider tokenProvider)
    {
        _authService = authService;
        _tokenProvider = tokenProvider;
    }
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        LoginRequestDTO loginRequestDTO = new();

        loginRequestDTO.ReturnUrl = returnUrl;

        return View(loginRequestDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
    {
        var authRequest = new UserLoginRequest(loginRequestDTO);

        var apiResponse = await _authService.Login(authRequest);

        if (!apiResponse.IsSuccessStatusCode)
        {
            var errorResponse = apiResponse.Error.Content;

            TempData["error"] = errorResponse ?? "Error occured";

            return RedirectToAction(nameof(Login));
        }

        var customApiResponse = apiResponse.Content;

        var responseDto = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(customApiResponse.Result));

        // embedd jwt token into newly created cookie
        _tokenProvider.SetToken(responseDto.AccessToken);

        await SignInUser(responseDto);

        return LocalRedirect(loginRequestDTO.ReturnUrl);
    }

    public IActionResult Register(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        RegistrationRequestDTO registrationRequestDTO = new();

        registrationRequestDTO.ReturnUrl = returnUrl;

        return View(registrationRequestDTO);
    }


    [HttpPost]
    public async Task<IActionResult> Register(RegistrationRequestDTO registrationRequestDTO)
    {
        var authRequest = new UserRegisterRequest(registrationRequestDTO);

        var apiResponse = await _authService.Register(authRequest);

        if (!apiResponse.IsSuccessStatusCode)
        {
            var errorResponse = apiResponse.Error.Content;

            TempData["error"] = errorResponse ?? "Error occured";

            return RedirectToAction(nameof(Register));
        }

        return !string.IsNullOrEmpty(registrationRequestDTO.ReturnUrl) ? LocalRedirect(registrationRequestDTO.ReturnUrl) : RedirectToAction(nameof(Login));
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        _tokenProvider.ClearToken();

        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }


    #region HELPERMETHODS

    private async Task SignInUser(LoginResponseDTO loginResponseDTO)
    {
        // token
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(loginResponseDTO.AccessToken);

        // authentication cookie, adding claims from the token
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, jwtToken.Claims.FirstOrDefault(l => l.Type == JwtRegisteredClaimNames.Sub).Value));
        identity.AddClaim(new Claim(ClaimTypes.Role, jwtToken.Claims.FirstOrDefault(l => l.Type == "role").Value));
        identity.AddClaim(new Claim(ClaimTypes.Name, jwtToken.Claims.FirstOrDefault(l => l.Type == JwtRegisteredClaimNames.Name).Value));
        identity.AddClaim(new Claim(ClaimTypes.Email, jwtToken.Claims.FirstOrDefault(l => l.Type == JwtRegisteredClaimNames.Email).Value));

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }

    #endregion
}
