using GeneralUsing.CQRS;

namespace OrderingService.Application.Orders.Queries.GetAllOrders;
public record GetAllOrdersQuery(Guid? CustomerId = null) : IQuery<CustomApiResponse>;


