using FluentValidation;
using GeneralUsing.CQRS;
using Services.OrderingService.OrderingService.Application.DTOs;

namespace Services.OrderingService.OrderingService.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(CustomerDTO CustomerDTO) : ICommand<CustomApiResponse>;
//public record CreateCustomerResult(Customer Customer);

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    // TOTO IMPLEMENTATION
}


