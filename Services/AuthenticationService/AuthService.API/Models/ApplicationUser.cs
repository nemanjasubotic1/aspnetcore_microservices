using Microsoft.AspNetCore.Identity;

namespace AuthService.API.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public string StreetAddress { get; set; }
}
