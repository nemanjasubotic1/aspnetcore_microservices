using GeneralUsing.CQRS;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OrderingService.Application.Data;
using OrderingService.Application.DTOs;
using OrderingService.Domain.Models;

namespace OrderingService.Application.Customers.Queries.GetCustomerById;
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
