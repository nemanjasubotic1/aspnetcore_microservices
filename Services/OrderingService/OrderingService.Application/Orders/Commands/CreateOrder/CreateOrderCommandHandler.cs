﻿using GeneralUsing.CQRS;
using Mapster;
using MediatR;
using OrderingService.Application.DTOs;
using OrderingService.Domain.Models;
using Services.OrderingService.OrderingService.Application.Customers.Commands.CreateCustomer;
using Services.OrderingService.OrderingService.Application.Customers.Queries.GetCustomerById;
using Services.OrderingService.OrderingService.Application.Data;
using Services.OrderingService.OrderingService.Domain;
using Services.OrderingService.OrderingService.Domain.Models;
using Services.OrderingService.OrderingService.Domain.ValueModels;

namespace Services.OrderingService.OrderingService.Application.Orders.Commands.CreateOrder;
public class CreateOrderCommandHandler(IAppDbContext dbContext, ISender sender) : ICommandHandler<CreateOrderCommand, CustomApiResponse>
{
    public async Task<CustomApiResponse> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderHeader = new OrderHeader();

        var customerQuery = await sender.Send(new GetCustomerByIdQuery(command.CustomerDTO.Id));

        if (customerQuery != null && customerQuery.DoesExist && customerQuery.Customer != null)
        {
            orderHeader = await CreateNewOrder(command.OrderHeaderDTO, customerQuery.Customer);
        }
        else
        {
            // create new customer first 
            var newCustomerCommand = await sender.Send(new CreateCustomerCommand(command.CustomerDTO));

            var customerFromDb = newCustomerCommand.Result as Customer;

            //var customer = JsonConvert.DeserializeObject<Customer>(customerFromDb);

            orderHeader = await CreateNewOrder(command.OrderHeaderDTO, customerFromDb);
        }

        dbContext.OrderHeaders.Add(orderHeader);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);

            foreach (var orderDetails in orderHeader.OrderDetails)
            {
                orderDetails.OrderHeaderId = orderHeader.Id;
            }

            await dbContext.SaveChangesAsync(cancellationToken);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message.ToString());
        }

        return new CustomApiResponse(orderHeader.Id);

    }

    private async Task<OrderHeader> CreateNewOrder(OrderHeaderDTO orderHeaderDTO, Customer customer)
    {
        var billingAddress = new Address
        {
            FirstName = orderHeaderDTO.FirstName,
            LastName = orderHeaderDTO.LastName,
            EmailAddress = orderHeaderDTO.EmailAddress,
            AddressLine = orderHeaderDTO.AddressLine,
            Country = orderHeaderDTO.Country,
            State = orderHeaderDTO.State,
            ZipCode = orderHeaderDTO.ZipCode,
        };

        var payment = new Payment
        {
            CardName = orderHeaderDTO.CardName,
            CardNumber = orderHeaderDTO.CardNumber,
            Expiration = orderHeaderDTO.Expiration,
            CVV = orderHeaderDTO.CVV,
        };


        var orderHeader = OrderHeader.Create(
            address: billingAddress,
            orderStatus: OrderStatus.Pending,
            payment: payment,
            customerId: customer.Id
            );


        orderHeader.OrderDetails = [];

        foreach (var orderDetailsDto in orderHeaderDTO.OrderDetails)
        {
            if (orderDetailsDto != null)
            {
                var orderDetails = orderDetailsDto.Adapt<OrderDetails>();

                orderHeader.OrderDetails.Add(orderDetails);

                orderHeader.TotalPrice += orderDetailsDto.Quantity * orderDetailsDto.Price;
            }
        }

        orderHeader.Discount = orderHeaderDTO.Discount;
        orderHeader.TotalPrice -= orderHeaderDTO.Discount;

        customer.NumberOfOrders++;
        customer.LastOrder = DateTime.Now;
        customer.SpentMoney += orderHeader.TotalPrice;

        return orderHeader;
    }
}
