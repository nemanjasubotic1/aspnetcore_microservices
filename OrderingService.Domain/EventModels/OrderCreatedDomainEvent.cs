using OrderingService.Domain.AbstractModels;
using OrderingService.Domain.Models;

namespace OrderingService.Domain.EventModels;
public record OrderCreatedDomainEvent(OrderHeader OrderHeader) : IDomainEvent;
