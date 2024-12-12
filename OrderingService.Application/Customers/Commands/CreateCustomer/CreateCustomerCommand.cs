using FluentValidation;
using GeneralUsing.CQRS;
using OrderingService.Application.DTOs;
using OrderingService.Domain.Models;

namespace OrderingService.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(CustomerDTO CustomerDTO) : ICommand<CreateCustomerResult>;
public record CreateCustomerResult(Customer Customer);

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    // TOTO IMPLEMENTATION
}


