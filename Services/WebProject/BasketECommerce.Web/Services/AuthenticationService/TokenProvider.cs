namespace BasketECommerce.Web.Services.AuthenticationService;

public class TokenProvider : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IConfiguration _configuration;

    private string cookieToken;

    public TokenProvider(IHttpContextAccessor contextAccessor, IConfiguration configuration)
    {
        _contextAccessor = contextAccessor;
        _configuration = configuration;

        cookieToken = _configuration["JwtCookieName"];
    }
    
    public void ClearToken()
    {
        _contextAccessor.HttpContext?.Response.Cookies.Delete(cookieToken);
    }
    public void SetToken(string token)
    {
        _contextAccessor.HttpContext?.Response.Cookies.Append(cookieToken, token);
    }

    public string? GetToken()
    {
        string? token = null;

        bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(cookieToken, out token);

        return hasToken is true ? token : null;

    }

}
