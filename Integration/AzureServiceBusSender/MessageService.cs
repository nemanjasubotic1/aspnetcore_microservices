using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace Integration.AzureServiceBusSender;
public class MessageService : IMessageService
{
    private string connectionStrings =
        "Endpoint=sb://mangowebsolutions.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=wap4Qc3+PiiuoBTOIkoQQg9SlMA3ITus5+ASbOrrCRU=";

    public async Task PublishMessage(object message, string topic_queue_name)
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
