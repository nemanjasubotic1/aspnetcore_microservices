using Microsoft.EntityFrameworkCore;
using Services.EmailService.EmailService.API.Models;

namespace Services.EmailService.EmailService.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<EmailLogger> EmailLoggers { get; set; }
}
