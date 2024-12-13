using BasketECommerce.Web.Models;
using BasketECommerce.Web.Models.Authenication;
using BasketECommerce.Web.Models.Authentication;
using Refit;

namespace BasketECommerce.Web.Services.AuthenticationService;

public interface IAuthService
{

    [Post("/userauth/login")]
    Task<ApiResponse<CustomApiResponse>> Login(UserLoginRequest userLoginRequest);
    
    
    [Post("/userauth/register")]
    Task<ApiResponse<CustomApiResponse>> Register(UserRegisterRequest userRegisterRequest);


}
