using GeneralUsing.CQRS;
using OrderingService.Application.DTOs;
using Services.OrderingService.OrderingService.Application.DTOs;
using System.Windows.Input;

namespace Services.OrderingService.OrderingService.Application.Customers.Commands.UpdateCustomerCommand;

public record UpdateCustomerCommand(CustomerDTO CustomerDTO) : ICommand<CustomApiResponse>;
//public record UpdateCustomerCommandResult(bool IsSuccess);

// TOTO Validators
