using GeneralUsing.CQRS;

namespace Services.OrderingService.OrderingService.Application.Orders.Queries.GetOrderByCustomerId;

public record GetOrderByUserIdQuery(Guid UserId) : IQuery<CustomApiResponse>;

