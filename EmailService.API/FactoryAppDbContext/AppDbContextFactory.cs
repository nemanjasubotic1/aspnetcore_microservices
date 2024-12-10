using EmailService.API.Data;
using Microsoft.EntityFrameworkCore;

namespace EmailService.API.FactoryAppDbContext;

public class AppDbContextFactory : IAppDbContextFactory
{

    private readonly DbContextOptions<AppDbContext> _options;

    public AppDbContextFactory(DbContextOptions<AppDbContext> options)
    {
        _options = options;
    }

    public AppDbContext Create()
    {
        return new AppDbContext(_options);
    }
}
