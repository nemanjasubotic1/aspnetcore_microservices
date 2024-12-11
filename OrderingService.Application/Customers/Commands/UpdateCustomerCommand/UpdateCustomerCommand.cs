using GeneralUsing.CQRS;
using OrderingService.Application.DTOs;
using System.Windows.Input;

namespace OrderingService.Application.Customers.Commands.UpdateCustomerCommand;

public record UpdateCustomerCommand(CustomerDTO CustomerDTO) : ICommand<UpdateCustomerCommandResult>;
public record UpdateCustomerCommandResult(bool IsSuccess);

// TOTO Validators
