using FluentValidation;
using GeneralUsing.CQRS;
using OrderingService.Application.DTOs;

namespace OrderingService.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(CustomerDTO CustomerDTO) : ICommand<CreateCustomerResult>;
public record CreateCustomerResult(Guid Id);

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    // TOTO IMPLEMENTATION
}


