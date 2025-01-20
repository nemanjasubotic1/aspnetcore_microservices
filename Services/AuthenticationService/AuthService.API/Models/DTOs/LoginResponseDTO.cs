namespace AuthService.API.Models.DTOs;

public class LoginResponseDTO
{
    public UserDTO UserDTO { get; set; }
    public string AccessToken { get; set; }
}
