using MediatR;
using Microsoft.Extensions.Logging;
using Services.OrderingService.OrderingService.Domain.EventModels;

namespace Services.OrderingService.OrderingService.Application.Orders.EventHandlers;
public class OrderCreateEventHandler(ILogger<OrderCreateEventHandler> _logger) : INotificationHandler<OrderCreatedDomainEvent>
{
    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Domain event handled from {notification.GetType().Name}");
    }
}
