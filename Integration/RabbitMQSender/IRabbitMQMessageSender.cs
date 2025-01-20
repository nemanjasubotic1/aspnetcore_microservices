namespace Integration.RabbitMQSender;
public interface IRabbitMQMessageSender
{
    Task SendMessage(object message, string queueName);
}
