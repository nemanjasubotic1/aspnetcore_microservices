using AuthService.API.Data;
using AuthService.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService.API.Utility;

public class DbInitializer : IDbInitializer
{
    private readonly AppDbContext _db;

    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly UserManager<ApplicationUser> _userManager;

    public DbInitializer(AppDbContext db, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _roleManager = roleManager;
        _userManager = userManager;
    }
    public async Task InitializeAsync()
    {
        if (_db.Database.GetPendingMigrations().Any())
        {
            _db.Database.Migrate();
        }

        if (!await _roleManager.RoleExistsAsync(SD.Admin_Role))
        {
            await _roleManager.CreateAsync(new IdentityRole(SD.Admin_Role));
            await _roleManager.CreateAsync(new IdentityRole(SD.Customer_Role));

            ApplicationUser user = new()
            {
                Name = "Main Admin",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                PhoneNumber = "1234567890",
            };

            var result = await _userManager.CreateAsync(user, "Admin123");

            if (result.Succeeded)
            {
                ApplicationUser userFromDb = await _db.ApplicationUsers.FirstOrDefaultAsync(l => l.UserName == "admin@gmail.com");
                await _userManager.AddToRoleAsync(userFromDb, SD.Admin_Role);
            }
        }
    }
}
