using EmailService.API.Messaging.AzureServiceBusConsumer;
using Microsoft.Extensions.DependencyInjection;
using System.Security.AccessControl;

namespace EmailService.API.Extensions;

public static class ApplicationBuilderExtensions
{
    private static IServiceBusConsumer ServiceBusConsumer { get; set; }

    public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
    {
        ServiceBusConsumer = app.ApplicationServices.GetService<IServiceBusConsumer>();

        var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();
        hostApplicationLife.ApplicationStarted.Register(OnStart);
        hostApplicationLife.ApplicationStopped.Register(OnStop);

        return app;
    }
    private static void OnStart()
    {
        ServiceBusConsumer.Start();
    }

    private static void OnStop()
    {
        ServiceBusConsumer.Stop();
    }

}
