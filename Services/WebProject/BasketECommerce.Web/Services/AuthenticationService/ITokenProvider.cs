namespace BasketECommerce.Web.Services.AuthenticationService;

public interface ITokenProvider
{
    void ClearToken();
    void SetToken(string token);
    string? GetToken();
}
