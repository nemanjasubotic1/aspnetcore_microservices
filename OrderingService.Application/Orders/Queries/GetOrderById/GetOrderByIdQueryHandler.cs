using FluentValidation.Results;
using GeneralUsing.CQRS;
using Microsoft.EntityFrameworkCore;
using OrderingService.Application.Data;
using OrderingService.Application.Extensions;

namespace OrderingService.Application.Orders.Queries.GetOrderById;
public class GetOrderByIdQueryHandler(IAppDbContext dbContext) : IQueryHandler<GetOrderByIdQuery, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var orderHeader = await dbContext.OrderHeaders.Include(l => l.OrderDetails).FirstOrDefaultAsync(l => l.Id == request.Id);

        if (orderHeader == null)
        {
            return new CustomApiResponse(null, false, [new ValidationFailure("", "Order dont exist.")]);
        }

        return new CustomApiResponse(orderHeader.OrderToOrderDTO());
    }
}
