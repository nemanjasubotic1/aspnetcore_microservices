using CouponService.API.Models;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;

namespace CouponService.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Coupon> Coupons { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
