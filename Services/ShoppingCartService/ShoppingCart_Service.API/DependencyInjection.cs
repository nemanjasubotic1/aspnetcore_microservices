using Carter;
using FluentValidation;
using GeneralUsing.Exceptions;
using GeneralUsing.MediatorPipelineBehaviors;
using Integration.AzureServiceBusSender;
using Main.ShoppingCartService.ShoppingCart_Service.API.Data;
using Main.ShoppingCartService.ShoppingCart_Service.API.Models;
using Marten;

namespace Main.ShoppingCartService.ShoppingCart_Service.API;

public static class DependencyInjection
{
    public static IServiceCollection AddShoppingCartServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddCarter();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            //config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(FluentValidationBehavior<,>));

        });

        services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        services.AddMarten(options =>
        {
            options.Connection(configuration.GetConnectionString("DefaultConnection")!);

            options.Schema.For<ShoppingCart>().Identity(l => l.Id).SoftDeleted();

        }).UseLightweightSessions();

        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
        services.Decorate<IShoppingCartRepository, CachedShoppingCartRepository>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["Redis"];
            options.InstanceName = "ShoppingCart_";
        });
       
        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddScoped<IMessageService, MessageService>();

        return services;
    }

    public static WebApplication UseShoppingCartServices(this WebApplication app)
    {
        app.MapCarter();

        app.UseExceptionHandler(options => { });

        return app;
    }

}
