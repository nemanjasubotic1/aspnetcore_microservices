using Azure.Core.Extensions;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace Integration.AzureServiceBusSender;
public class MessageService : IMessageService
{
    
    public async Task PublishMessage(object message, string topic_queue_name, string connectionStrings)
    {
        await using var client = new ServiceBusClient(connectionStrings);

        ServiceBusSender sender = client.CreateSender(topic_queue_name);

        var jsonMessage = JsonConvert.SerializeObject(message);

        ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
        {
            CorrelationId = Guid.NewGuid().ToString()
        };

        await sender.SendMessageAsync(finalMessage);

        await client.DisposeAsync();    
    }
}
