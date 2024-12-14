using GeneralUsing.CQRS;
using System.Windows.Input;

namespace OrderingService.Application.Orders.Commands.DeleteOrder;
public record DeleteOrderCommand(Guid Id) : ICommand<CustomApiResponse>;
//public record DeleteOrderResult(bool IsSuccess);

