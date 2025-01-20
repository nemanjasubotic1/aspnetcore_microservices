using Carter;
using FluentValidation;
using GeneralUsing.Exceptions;
using GeneralUsing.Extensions;
using GeneralUsing.MediatorPipelineBehaviors;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Main.ProductService.ProductCategory.API.InitialData;

public static class DependencyInjection
{
    public static IServiceCollection AddProductServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMarten(options =>
        {
            options.Connection(configuration.GetConnectionString("DefaultConnection")!);

            options.Schema.For<Category>().Identity(l => l.Id).SoftDeleted();
            options.Schema.For<Product>().Identity(l => l.Id).Index(l => l.CategoryId);

        }).UseLightweightSessions();

        services.AddAppAuthentication(configuration);

        services.AddCarter();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            //config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(FluentValidationBehavior<,>));

        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["Redis"];
            options.InstanceName = "ProductCategory_";
        });

        services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("DefaultConnection")!);

        services.AddAuthorization();

        return services;
    }

    public static WebApplication UseProductServices(this WebApplication app)
    {
        app.MapCarter();

        app.UseExceptionHandler(options => { });

        app.UseHealthChecks("/health",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            });

        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }


}
