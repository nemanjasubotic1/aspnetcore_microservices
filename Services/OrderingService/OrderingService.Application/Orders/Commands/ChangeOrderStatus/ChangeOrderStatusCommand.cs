using GeneralUsing.CQRS;

namespace Services.OrderingService.OrderingService.Application.Orders.Commands.ChangeOrderStatus;
public record ChangeOrderStatusCommand(Guid OrderId, string status) : ICommand<CustomApiResponse>;


