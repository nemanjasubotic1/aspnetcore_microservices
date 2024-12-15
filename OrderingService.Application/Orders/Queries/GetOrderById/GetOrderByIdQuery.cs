using GeneralUsing.CQRS;

namespace OrderingService.Application.Orders.Queries.GetOrderById;
public record GetOrderByIdQuery(Guid Id) : IQuery<CustomApiResponse>;


