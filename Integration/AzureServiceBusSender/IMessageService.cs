namespace Integration.AzureServiceBusSender;
public interface IMessageService
{
    Task PublishMessage(object message, string topic_queue_name);
}
