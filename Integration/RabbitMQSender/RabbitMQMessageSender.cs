using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Integration.RabbitMQSender;
public class RabbitMQMessageSender : IRabbitMQMessageSender
{
    private readonly string _hostname;
    private readonly string _username;
    private readonly string _password;

    private IConnection _connection;

    public RabbitMQMessageSender()
    {
        _hostname = "localhost";
        _username = "guest";
        _password = "guest";
    }
    public async Task SendMessage(object message, string queueName)
    {

        if (await IfConnectionExist())
        {
            using var channel = await _connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queueName, false, false, false, null);

            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body);
        }
    }

    private async Task CreateConnection()
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            _connection = await factory.CreateConnectionAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task<bool> IfConnectionExist()
    {
        if (_connection != null)
        {
            return true;
        }
        await CreateConnection();
        return true;
    }
}
