namespace BasketECommerce.Web.Models.Authenication;

public class LoginResponseDTO
{
    public UserDTO UserDTO { get; set; }
    public string AccessToken { get; set; }
}
