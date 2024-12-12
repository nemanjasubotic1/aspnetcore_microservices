using FluentValidation;
using GeneralUsing.CQRS;
using OrderingService.Application.DTOs;

namespace OrderingService.Application.Orders.Commands.CreateOrder;
public record CreateOrderCommand(OrderHeaderDTO OrderHeaderDTO, CustomerDTO CustomerDTO) : ICommand<CreateOrderResult>;
public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(l => l.OrderHeaderDTO.OrderDetails).NotEmpty().WithMessage("Order details is required");
        RuleFor(l => l.OrderHeaderDTO.Payment).NotEmpty().WithMessage("Payment details should not be empty");

        // TODO Add more Validations
    }
}

