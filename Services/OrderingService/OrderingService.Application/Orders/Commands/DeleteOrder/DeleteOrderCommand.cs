using GeneralUsing.CQRS;

namespace Services.OrderingService.OrderingService.Application.Orders.Commands.DeleteOrder;
public record DeleteOrderCommand(Guid Id) : ICommand<CustomApiResponse>;
//public record DeleteOrderResult(bool IsSuccess);

