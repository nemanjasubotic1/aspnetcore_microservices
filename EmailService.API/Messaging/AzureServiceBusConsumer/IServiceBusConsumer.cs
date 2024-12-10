namespace EmailService.API.Messaging.AzureServiceBusConsumer;

public interface IServiceBusConsumer
{
    Task Start();
    Task Stop();
}
