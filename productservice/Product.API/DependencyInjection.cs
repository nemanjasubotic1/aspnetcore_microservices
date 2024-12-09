using Carter;
using FluentValidation;
using GeneralUsing.Exceptions;
using GeneralUsing.MediatorPipelineBehaviors;
using Marten;
using ProductCategory.API.Data;
using ProductCategory.API.Models;

namespace ProductCategory.API;

public static class DependencyInjection
{
    public static IServiceCollection AddProductServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddCarter();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            //config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(FluentValidationBehavior<,>));

        });

        services.AddMarten(options =>
        {
            options.Connection(configuration.GetConnectionString("DefaultConnection")!);

            options.Schema.For<Category>().Identity(l => l.Id);
            options.Schema.For<Product>().Identity(l => l.Id).Index(l => l.CategoryId);

        }).UseLightweightSessions();

        services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddExceptionHandler<CustomExceptionHandler>();

        return services;
    }

    public static WebApplication UseProductServices(this WebApplication app)
    {
        app.MapCarter();

        app.UseExceptionHandler(options => { });

        return app;
    }


}
