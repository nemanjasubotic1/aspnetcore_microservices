using GeneralUsing.CQRS;
using System.Windows.Input;

namespace OrderingService.Application.Orders.Commands.DeleteOrder;
public record DeleteOrderCommand(Guid Id) : ICommand<DeleteOrderResult>;
public record DeleteOrderResult(bool IsSuccess);

