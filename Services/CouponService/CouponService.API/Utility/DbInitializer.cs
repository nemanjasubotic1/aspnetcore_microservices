using CouponService.API.Data;

namespace CouponService.API.Utility;

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
