using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.OrderingService.OrderingService.API.Infrastructure.Data;
using Services.OrderingService.OrderingService.API.Infrastructure.Data.Interceptors;
using Services.OrderingService.OrderingService.Application.Data;

namespace Services.OrderingService.OrderingService.API.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<ISaveChangesInterceptor, DomainEventInterceptors>();
        services.AddScoped<ISaveChangesInterceptor, EntityInterceptors>();

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        });

        services.AddScoped<IAppDbContext, AppDbContext>();

        return services;
    }
}
