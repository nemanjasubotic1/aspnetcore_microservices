using GeneralUsing.CQRS;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OrderingService.Application.Data;
using OrderingService.Application.DTOs;

namespace OrderingService.Application.Orders.Queries.GetOrderByCustomerId;


public class GetOrderByCustomerIdQueryHandler(IAppDbContext dbContext) : IQueryHandler<GetOrderByCustomerIdQuery, GetOrderByCustimerIdResult>
{
    public async Task<GetOrderByCustimerIdResult> Handle(GetOrderByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        var orderFromDb = await dbContext.OrderHeaders.FirstOrDefaultAsync(l => l.CustomerId == request.CustomerId);

        if (orderFromDb == null)
        {
            return new GetOrderByCustimerIdResult();
        }

        var orderDto = orderFromDb.Adapt<OrderHeaderDTO>();

        return new GetOrderByCustimerIdResult(orderDto);
    }
}
