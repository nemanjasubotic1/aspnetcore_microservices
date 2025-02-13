﻿using GeneralUsing.CQRS;
using Microsoft.EntityFrameworkCore;
using Services.OrderingService.OrderingService.Application.Data;

namespace Services.OrderingService.OrderingService.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IAppDbContext dbContext) : ICommandHandler<DeleteOrderCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderFromDb = await dbContext.OrderHeaders.FirstOrDefaultAsync(l => l.Id == request.Id);

        if (orderFromDb == null)
        {
            return new CustomApiResponse(null, false);
        }

        dbContext.OrderHeaders.Remove(orderFromDb);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CustomApiResponse();
    }
}
