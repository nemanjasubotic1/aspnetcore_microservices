using GeneralUsing.CQRS;
using Microsoft.EntityFrameworkCore;
using Services.OrderingService.OrderingService.Application.Data;
using Services.OrderingService.OrderingService.Application.Extensions;

namespace Services.OrderingService.OrderingService.Application.Orders.Queries.GetAllOrders;
public class GetAllOrdersQueryHandler(IAppDbContext dbContext) : IQueryHandler<GetAllOrdersQuery, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orderDTOlist = await dbContext.OrderHeaders.ToListAsync();

        if (request.CustomerId != null)
        {
            orderDTOlist =  orderDTOlist.Where(l => l.CustomerId == request.CustomerId).ToList();
        }

        var orderHeaderDTOlist = orderDTOlist.OrdersListToOrdersDtoList();

        return new CustomApiResponse(orderHeaderDTOlist);
    }
}
