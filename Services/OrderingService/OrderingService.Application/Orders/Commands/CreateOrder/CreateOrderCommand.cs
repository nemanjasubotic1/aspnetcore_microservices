﻿using FluentValidation;
using GeneralUsing.CQRS;
using OrderingService.Application.DTOs;
using Services.OrderingService.OrderingService.Application.DTOs;

namespace Services.OrderingService.OrderingService.Application.Orders.Commands.CreateOrder;
public record CreateOrderCommand(OrderHeaderDTO OrderHeaderDTO, CustomerDTO CustomerDTO) : ICommand<CustomApiResponse>;
public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(l => l.OrderHeaderDTO.OrderDetails).NotEmpty().WithMessage("Order details is required");

        // TODO Add more Validations
    }
}

