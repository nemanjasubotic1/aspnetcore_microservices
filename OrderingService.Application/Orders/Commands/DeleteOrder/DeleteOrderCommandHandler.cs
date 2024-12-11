using GeneralUsing.CQRS;
using Microsoft.EntityFrameworkCore;
using OrderingService.Application.Data;

namespace OrderingService.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IAppDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderFromDb = await dbContext.OrderHeaders.FirstOrDefaultAsync(l => l.Id == request.Id);

        if (orderFromDb == null)
        {
            return new DeleteOrderResult(false);
        }

        dbContext.OrderHeaders.Remove(orderFromDb);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResult(true);
    }
}
