using GeneralUsing.CQRS;
using OrderingService.Application.DTOs;
using OrderingService.Domain.Models;

namespace OrderingService.Application.Customers.Queries.GetCustomerById;
public record GetCustomerByIdQuery(Guid Id) : IQuery<GetCustomerByIdResult>;
public record GetCustomerByIdResult(bool DoesExist, Customer Customer = null);

