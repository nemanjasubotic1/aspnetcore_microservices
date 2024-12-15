using GeneralUsing.CQRS;
using OrderingService.Application.DTOs;

namespace OrderingService.Application.Orders.Queries.GetOrderByCustomerId;

public record GetOrderByUserIdQuery(Guid UserId) : IQuery<CustomApiResponse>;

