using FluentValidation.Results;
using GeneralUsing.CQRS;
using Microsoft.EntityFrameworkCore;
using OrderingService.Application.Data;
using OrderingService.Application.DTOs;
using OrderingService.Application.Extensions;
using OrderingService.Domain.Models;

namespace OrderingService.Application.Orders.Queries.GetAllOrders;
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
