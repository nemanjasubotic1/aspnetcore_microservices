using Carter;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OrderingService.Application.Data;
using OrderingService.Infrastructure.Data.Interceptors;

namespace OrderingService.API;

public static class DependecyInjection
{
    public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddCarter();

        services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        return services;
    }


    public static WebApplication UserAPIServices(this WebApplication app)
    {
        app.MapCarter();

        return app;
    }
}
