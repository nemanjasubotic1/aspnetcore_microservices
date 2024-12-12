using GeneralUsing.CQRS;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OrderingService.Application.Data;
using OrderingService.Application.DTOs;
using OrderingService.Domain.Models;

namespace OrderingService.Application.Customers.Queries.GetCustomerById;
public class GetCustomerByUserIdQueryHandler(IAppDbContext dbContext) : IQueryHandler<GetCustomerByUserIdQuery, GetCustomerByUserIdResult>
{
    public async Task<GetCustomerByUserIdResult> Handle(GetCustomerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var customerFromDb = await dbContext.Customers.FirstOrDefaultAsync(l => l.UserId == request.UserId);

        if (customerFromDb == null)
        {
            return new GetCustomerByUserIdResult(false, null);
        }


        return new GetCustomerByUserIdResult(true, customerFromDb);

    }
}
