﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OrderingService.Domain.AbstractModels;

namespace OrderingService.Infrastructure.Data.Interceptors;
public class DomainEventInterceptors : SaveChangesInterceptor
{
    private readonly IMediator _mediator;
    public DomainEventInterceptors(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }


    private async Task DispatchDomainEvents(DbContext? dbContext)
    {
        if (dbContext == null) return;

        var domainEventCreators = dbContext.ChangeTracker.Entries<Aggregate>()
            .Where(l => l.Entity.DomainEvents.Any())
            .Select(l => l.Entity);

        var domainEvents = domainEventCreators.SelectMany(l => l.DomainEvents).ToList();

        foreach ( var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);
        }

    }
}
