using Services.OrderingService.OrderingService.Domain.AbstractModels;
using Services.OrderingService.OrderingService.Domain.Models;

namespace Services.OrderingService.OrderingService.Domain.EventModels;
public record OrderCreatedDomainEvent(OrderHeader OrderHeader) : IDomainEvent;
