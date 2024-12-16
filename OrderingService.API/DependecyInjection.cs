using Carter;
using FluentValidation;
using GeneralUsing.Exceptions;
using GeneralUsing.Extensions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OrderingService.Application.Data;
using OrderingService.Infrastructure.Data.Interceptors;

namespace OrderingService.API;

public static class DependecyInjection
{
    public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAppAuthentication(configuration);

        services.AddAuthorization();

        services.AddCarter();

        services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        services.AddExceptionHandler<CustomExceptionHandler>();


        return services;
    }

    public static WebApplication UserAPIServices(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseExceptionHandler(options => { });

        app.MapCarter();

        return app;
    }
}
