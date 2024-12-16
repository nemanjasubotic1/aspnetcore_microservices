using Services.EmailService.EmailService.API.Messaging.AzureServiceBusConsumer;

namespace Services.EmailService.EmailService.API.Extensions;

public static class ApplicationBuilderExtensions
{
    private static IServiceBusConsumer ServiceBusConsumer { get; set; }

    public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
    {
        ServiceBusConsumer = app.ApplicationServices.GetService<IServiceBusConsumer>()!;

        var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();
        hostApplicationLife!.ApplicationStarted.Register(OnStart);
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
