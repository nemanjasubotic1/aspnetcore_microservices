using Microsoft.EntityFrameworkCore;
using OrderingService.Application.Data;
using OrderingService.Domain.Models;
using System.Reflection;

namespace OrderingService.Infrastructure.Data;
public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<OrderHeader> OrderHeaders{ get; set; }
    public DbSet<OrderDetails> OrderDetails{ get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
