using MediatR;
using Microsoft.Extensions.Logging;
using OrderingService.Domain.EventModels;

namespace OrderingService.Application.Orders.EventHandlers;
public class OrderCreateEventHandler(ILogger<OrderCreateEventHandler> _logger) : INotificationHandler<OrderCreatedDomainEvent>
{
    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Domain event handled from {notification.GetType().Name}");
    }
}
