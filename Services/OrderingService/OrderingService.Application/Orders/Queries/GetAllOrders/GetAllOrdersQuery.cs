using GeneralUsing.CQRS;

namespace Services.OrderingService.OrderingService.Application.Orders.Queries.GetAllOrders;
public record GetAllOrdersQuery(Guid? CustomerId = null) : IQuery<CustomApiResponse>;


