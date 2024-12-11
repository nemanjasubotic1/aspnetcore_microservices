using Azure.Messaging.ServiceBus;
using EmailService.API.Models.DTOs;
using EmailService.API.Services;
using Newtonsoft.Json;
using System.Text;

namespace EmailService.API.Messaging.AzureServiceBusConsumer;

public class ServiceBusConsumer : IServiceBusConsumer
{
    private readonly string _serviceBusConnectionString;
    private readonly string _shoppingCartTopic;
    private readonly string _shoppingCart_EmailSubscription;

    private ServiceBusProcessor _emailProcessor;

    private readonly IShoppingCartEmailService _shoppingCartEmailService;

    public ServiceBusConsumer(IConfiguration configuration, IShoppingCartEmailService shoppingCartEmailService)
    {
        _serviceBusConnectionString = configuration["AzureServiceBusConnectionStrings"]!;
        _shoppingCartTopic = configuration["TopicsAndQueueNames:ShoppingCartTopic"]!;
        _shoppingCart_EmailSubscription = configuration["TopicsAndQueueNames:ShoppingCart_EmailSubscription"]!;

        var client = new ServiceBusClient(_serviceBusConnectionString);

        _emailProcessor = client.CreateProcessor(_shoppingCartTopic, _shoppingCart_EmailSubscription);
        _shoppingCartEmailService = shoppingCartEmailService;
    }

    public async Task Start()
    {
        _emailProcessor.ProcessMessageAsync += OnEmailCartMessageReceived;
        _emailProcessor.ProcessErrorAsync += OnErrorOnMessageReceived;

        await _emailProcessor.StartProcessingAsync();
    }

    public async Task Stop()
    {
        await _emailProcessor.StopProcessingAsync();
        await _emailProcessor.DisposeAsync();
    }

    private async Task OnEmailCartMessageReceived(ProcessMessageEventArgs args)
    {
        var message = args.Message;
        var body = Encoding.UTF8.GetString(message.Body);

        ShoppingCartDTO shoppingCartDTO = JsonConvert.DeserializeObject<ShoppingCartDTO>(body);

        try
        {
            await _shoppingCartEmailService.SendAndStoreCart(shoppingCartDTO);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private Task OnErrorOnMessageReceived(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}
