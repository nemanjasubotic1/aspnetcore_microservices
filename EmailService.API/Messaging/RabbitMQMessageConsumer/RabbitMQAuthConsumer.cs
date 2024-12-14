using EmailService.API.Models.DTOs;
using EmailService.API.Services.RegistrationNotification;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EmailService.API.Messaging.RabbitMQMessageConsumer;

public class RabbitMQAuthConsumer : BackgroundService
{
    private readonly IRegistrationNotify _registrationNotify;

    private IConnection _connection;

    private IChannel _channel;

    private string queueName = "";

    public RabbitMQAuthConsumer(IConfiguration configuration, IRegistrationNotify registrationNotify)
    {
        _registrationNotify = registrationNotify;

        queueName = configuration["TopicsAndQueueNames:EmailRegistrationQueue"]!;
    }

    private async Task Initialization()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
        };

        _connection = await factory.CreateConnectionAsync();

        _channel = await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(queueName, false, false, false, null);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Initialization();

        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += OnReceiveMessage;

        await _channel.BasicConsumeAsync(queueName, false, consumer, cancellationToken: stoppingToken);
    }

    private async Task OnReceiveMessage(object sender, BasicDeliverEventArgs e)
    {
        var content = Encoding.UTF8.GetString(e.Body.ToArray());

        var registrationNotify = JsonConvert.DeserializeObject<RegistrationNotifyDTO>(content);

        await _registrationNotify.NewRegistrationNotification(registrationNotify);

        await _channel.BasicAckAsync(e.DeliveryTag, false);

    }

    
}
