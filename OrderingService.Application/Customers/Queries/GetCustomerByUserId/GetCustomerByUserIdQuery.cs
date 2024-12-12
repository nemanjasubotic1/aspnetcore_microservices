using GeneralUsing.CQRS;
using OrderingService.Application.DTOs;
using OrderingService.Domain.Models;

namespace OrderingService.Application.Customers.Queries.GetCustomerById;
public record GetCustomerByUserIdQuery(Guid UserId) : IQuery<GetCustomerByUserIdResult>;
public record GetCustomerByUserIdResult(bool DoesExist, Customer? Customer = null);

