﻿using FluentValidation.Results;
using GeneralUsing.CQRS;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OrderingService.Application.Data;
using OrderingService.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace OrderingService.Application.Orders.Queries.GetOrderByCustomerId;


public class GetOrderByCustomerIdQueryHandler(IAppDbContext dbContext) : IQueryHandler<GetOrderByCustomerIdQuery, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(GetOrderByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        var orderFromDb = await dbContext.OrderHeaders.FirstOrDefaultAsync(l => l.CustomerId == request.CustomerId);

        if (orderFromDb == null)
        {
            return new CustomApiResponse(null, false, [new ValidationFailure("", "Order dont exist")]);
        }

        var orderDto = orderFromDb.Adapt<OrderHeaderDTO>();

        return new CustomApiResponse(orderDto);
    }
}
