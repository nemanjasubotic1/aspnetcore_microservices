using FluentValidation.Results;
using GeneralUsing.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingService.Application.Data;
using OrderingService.Domain;

namespace OrderingService.Application.Orders.Commands.ChangeOrderStatus;
public class ChangeOrderStatusCommandHandler(IAppDbContext dbContext, ISender sender) : ICommandHandler<ChangeOrderStatusCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var orderFromDb = await dbContext.OrderHeaders.FirstOrDefaultAsync(l => l.Id == request.OrderId);

        if (orderFromDb == null)
            return new CustomApiResponse(null, false, [new ValidationFailure("", "Order dont exist.")]);

        orderFromDb.Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), request.status);

        dbContext.OrderHeaders.Update(orderFromDb);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CustomApiResponse();

    }
}
