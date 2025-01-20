using Microsoft.EntityFrameworkCore;
using OrderingService.Domain.Models;
using Services.OrderingService.OrderingService.Domain.Models;

namespace Services.OrderingService.OrderingService.Application.Data;
public interface IAppDbContext
{
    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<Customer> Customers { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
