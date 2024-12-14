using GeneralUsing.CQRS;
using OrderingService.Application.DTOs;

namespace OrderingService.Application.Orders.Queries.GetOrderByCustomerId;

public record GetOrderByCustomerIdQuery(Guid CustomerId) : IQuery<CustomApiResponse>;
//public record GetOrderByCustimerIdResult(OrderHeaderDTO? OrderHeaderDTO = null);

