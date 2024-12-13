using AuthService.API.Data;
using AuthService.API.Models;
using AuthService.API.Models.DTOs;
using AuthService.API.Services.IServices;
using AuthService.API.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace AuthService.API.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly AppDbContext _db;

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly IConfiguration _configuration;

    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IJwtTokenGenerator jwtTokenGenerator)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _jwtTokenGenerator = jwtTokenGenerator;
    }


    public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
    {
        var user = await _db.ApplicationUsers.FirstOrDefaultAsync(l => l.UserName.ToLower() == loginRequestDTO.Username.ToLower());

        bool isValidLogin = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

        if (!isValidLogin)
        {
            return new LoginResponseDTO
            {
                UserDTO = null,
                AccessToken = ""
            };
        }

        UserDTO userDTO = new()
        {
            Id = new Guid(user.Id),
            Username = user.UserName,
            EmailAddress = user.Email,
            Name = user.Name
        };

        var role = await _userManager.GetRolesAsync(user);

        LoginResponseDTO loginResponseDTO = new()
        {
            UserDTO = userDTO,
            AccessToken = _jwtTokenGenerator.GenerateToken(user, role.FirstOrDefault())
        };

        return loginResponseDTO;
    }

    public async Task<string> Register(RegistrationRequestDTO registrationRequestDTO)
    {
        ApplicationUser applicationUser = new()
        {
            Name = registrationRequestDTO.Name,
            UserName = registrationRequestDTO.Email,
            Email = registrationRequestDTO.Email,
            NormalizedUserName = registrationRequestDTO.Email.ToUpper(),
            NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
            PhoneNumber = registrationRequestDTO.PhoneNumber,
        };

        var userExist = await _db.ApplicationUsers.FirstOrDefaultAsync(l => l.Email.ToLower() == registrationRequestDTO.Email.ToLower());

        var roleExist = await _roleManager.RoleExistsAsync(registrationRequestDTO.RoleName);

        if (userExist != null)
        {
            return $"Error encountered, user with email {registrationRequestDTO.Email} already exist";
        }

        if (!roleExist)
        {
            return $"Error encountered, role {registrationRequestDTO.RoleName} dont exist";
        }

        try
        {
            var result = await _userManager.CreateAsync(applicationUser, registrationRequestDTO.Password);

            if (result.Succeeded)
            {
                var user = _db.ApplicationUsers.First(l => l.Email == registrationRequestDTO.Email);

                if (string.IsNullOrEmpty(registrationRequestDTO.RoleName))
                {
                    await _userManager.AddToRoleAsync(user, SD.Customer_Role);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, registrationRequestDTO.RoleName);
                }

                return "";
            }
            else
            {
                return result.Errors.FirstOrDefault().Description;
            }

        }
        catch (Exception e)
        {
            return "Error encountered: " + e.Message;
        }
    }
}

