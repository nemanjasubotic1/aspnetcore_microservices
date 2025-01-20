using Services.EmailService.EmailService.API.Data;

namespace Services.EmailService.EmailService.API.FactoryAppDbContext;

public interface IAppDbContextFactory
{
    AppDbContext Create();
}
