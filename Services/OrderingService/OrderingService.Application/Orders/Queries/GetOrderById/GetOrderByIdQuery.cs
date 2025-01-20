using GeneralUsing.CQRS;

namespace Services.OrderingService.OrderingService.Application.Orders.Queries.GetOrderById;
public record GetOrderByIdQuery(Guid Id) : IQuery<CustomApiResponse>;


