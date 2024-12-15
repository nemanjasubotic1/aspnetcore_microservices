using FluentValidation.Results;
using GeneralUsing.CQRS;
using Microsoft.EntityFrameworkCore;
using OrderingService.Application.Data;
using OrderingService.Application.DTOs;
using OrderingService.Application.Extensions;

namespace OrderingService.Application.Orders.Queries.GetAllOrders;
public class GetAllOrdersQueryHandler(IAppDbContext dbContext) : IQueryHandler<GetAllOrdersQuery, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orderDTOlist = await dbContext.OrderHeaders.ToListAsync();

        var orderHeaderDTOlist = orderDTOlist.OrdersListToOrdersDtoList();

        return new CustomApiResponse(orderHeaderDTOlist);
    }
}
