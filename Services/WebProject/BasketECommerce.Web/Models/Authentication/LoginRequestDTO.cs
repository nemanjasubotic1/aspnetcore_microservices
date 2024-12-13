namespace BasketECommerce.Web.Models.Authenication;

public class LoginRequestDTO
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ReturnUrl { get; set; }
}
