using MediatR;

namespace Services.OrderingService.OrderingService.Domain.AbstractModels;
public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccuredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}
