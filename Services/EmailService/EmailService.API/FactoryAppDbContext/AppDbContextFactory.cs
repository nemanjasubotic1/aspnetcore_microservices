using Microsoft.EntityFrameworkCore;
using Services.EmailService.EmailService.API.Data;

namespace Services.EmailService.EmailService.API.FactoryAppDbContext;

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

    AppDbContext IAppDbContextFactory.Create()
    {
        throw new NotImplementedException();
    }
}
