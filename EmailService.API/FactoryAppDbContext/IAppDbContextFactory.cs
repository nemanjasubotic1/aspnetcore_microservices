using EmailService.API.Data;

namespace EmailService.API.FactoryAppDbContext;

public interface IAppDbContextFactory
{
    AppDbContext Create();
}
