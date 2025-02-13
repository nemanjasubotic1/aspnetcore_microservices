﻿using GeneralUsing.CQRS;
using Microsoft.EntityFrameworkCore;
using Services.OrderingService.OrderingService.Application.Data;

namespace Services.OrderingService.OrderingService.Application.Customers.Queries.GetCustomerById;
public class GetCustomerByIdQueryHandler(IAppDbContext dbContext) : IQueryHandler<GetCustomerByIdQuery, GetCustomerByIdResult>
{
    public async Task<GetCustomerByIdResult> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customerFromDb = await dbContext.Customers.FirstOrDefaultAsync(l => l.Id == request.Id);

        if (customerFromDb == null)
        {
            return new GetCustomerByIdResult(false, null);
        }


        return new GetCustomerByIdResult(true, customerFromDb);

    }
}
