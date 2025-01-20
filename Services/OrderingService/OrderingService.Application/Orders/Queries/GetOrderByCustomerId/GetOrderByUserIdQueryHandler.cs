using FluentValidation.Results;
using GeneralUsing.CQRS;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OrderingService.Application.DTOs;
using Services.OrderingService.OrderingService.Application.Data;

namespace Services.OrderingService.OrderingService.Application.Orders.Queries.GetOrderByCustomerId;


public class GetOrderByUserIdQueryHandler(IAppDbContext dbContext) : IQueryHandler<GetOrderByUserIdQuery, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orderFromDb = await dbContext.OrderHeaders.FirstOrDefaultAsync(l => l.CustomerId == request.UserId);

        if (orderFromDb == null)
        {
            return new CustomApiResponse(null, false, [new ValidationFailure("", "Order dont exist")]);
        }

        var orderDto = orderFromDb.Adapt<OrderHeaderDTO>();

        return new CustomApiResponse(orderDto);
    }
}
