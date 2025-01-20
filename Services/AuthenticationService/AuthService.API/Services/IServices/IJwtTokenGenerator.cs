using AuthService.API.Models;

namespace AuthService.API.Services.IServices;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser user, string role);
}
