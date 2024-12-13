using AuthService.API.Models.DTOs;

namespace AuthService.API.Services.IServices;

public interface IAuthenticationService
{
    Task<string> Register(RegistrationRequestDTO registrationRequestDTO);
    Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
}
