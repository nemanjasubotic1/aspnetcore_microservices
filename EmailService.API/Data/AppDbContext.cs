using EmailService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailService.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<EmailLogger> EmailLoggers { get; set; }
}
