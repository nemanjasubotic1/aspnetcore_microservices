using GeneralUsing.CQRS;
using System.Windows.Input;

namespace OrderingService.Application.Orders.Commands.ChangeOrderStatus;
public record ChangeOrderStatusCommand(Guid OrderId, string status) : ICommand<CustomApiResponse>;


