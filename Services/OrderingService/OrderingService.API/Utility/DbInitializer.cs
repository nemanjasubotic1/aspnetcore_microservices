using Microsoft.EntityFrameworkCore;
using Services.OrderingService.OrderingService.API.Infrastructure.Data;

namespace OrderingService.API.Utility;

public class DbInitializer : IDbInitializer
{
    private readonly AppDbContext _db;

    public DbInitializer(AppDbContext db)
    {
        _db = db;
    }

    public void Initialize()
    {
        if (_db.Database.GetPendingMigrations().Any())
        {
            _db.Database.Migrate();
        }
    }
}
