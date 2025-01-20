namespace BasketECommerce.Web.Models.Authenication;

public class RegistrationRequestDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string? ReturnUrl { get; set; } 
}
