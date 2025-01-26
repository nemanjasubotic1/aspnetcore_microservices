using Microsoft.EntityFrameworkCore;
using Services.EmailService.EmailService.API.Data;

namespace EmailService.API.Utility;

public class DbInitializer : IDbInitializer
{
    private readonly AppDbContext _db;
    public DbInitializer(AppDbContext db)
    {
        _db = db;
    }
    public void Initialize()
    {
        _db.Database.EnsureCreated();
    }
}

