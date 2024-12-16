using GeneralUsing.CQRS;
using Services.OrderingService.OrderingService.Domain.Models;

namespace Services.OrderingService.OrderingService.Application.Customers.Queries.GetCustomerById;
public record GetCustomerByIdQuery(Guid Id) : IQuery<GetCustomerByIdResult>;
public record GetCustomerByIdResult(bool DoesExist, Customer Customer = null);

