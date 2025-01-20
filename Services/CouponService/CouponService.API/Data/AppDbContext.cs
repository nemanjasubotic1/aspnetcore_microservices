using CouponService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CouponService.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Coupon> Coupons { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(

            new Coupon { Id = 1, Amount = 50, ExpiryDate = DateTime.Now.AddDays(5), CouponName = "OkkMF50", IsEnabled = true },
            new Coupon { Id = 2, Amount = 100, ExpiryDate = DateTime.Now.AddDays(10), CouponName = "OkkMF100", IsEnabled = true }
        );
    }
}
