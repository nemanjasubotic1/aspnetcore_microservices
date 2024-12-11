using GeneralUsing.CQRS;
using Mapster;
using MediatR;
using OrderingService.Application.Customers.Commands.GetCustomerById;
using OrderingService.Application.Data;
using OrderingService.Application.DTOs;
using OrderingService.Domain;
using OrderingService.Domain.Models;
using OrderingService.Domain.ValueModels;

namespace OrderingService.Application.Orders.Commands.CreateOrder;
public class CreateOrderCommandHandler(IAppDbContext dbContext, ISender sender) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderHeader = new OrderHeader();

        var customerQuery = await sender.Send(new GetCustomerByIdQuery(command.OrderHeaderDTO.CustomerId));


        if (customerQuery != null && customerQuery.DoesExist && customerQuery.Customer != null)
        {

            orderHeader = await CreateNewOrder(command.OrderHeaderDTO, true, customerQuery.Customer);
        }
        else
        {
            // create new customer first ??
            orderHeader = await CreateNewOrder(command.OrderHeaderDTO, false);
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

        return new CreateOrderResult(orderHeader.Id);

    }

    private async Task<OrderHeader> CreateNewOrder(OrderHeaderDTO orderHeaderDTO, bool DoesExistCustomer = false, Customer? customer = null)
    {
        var billingAddress = new Address
        {
            FirstName = orderHeaderDTO.BillingAddress.FirstName,
            LastName = orderHeaderDTO.BillingAddress.LastName,
            EmailAddress = orderHeaderDTO.BillingAddress.EmailAddress,
            AddressLine = orderHeaderDTO.BillingAddress.AddressLine,
            Country = orderHeaderDTO.BillingAddress.Country,
            State = orderHeaderDTO.BillingAddress.State,
            ZipCode = orderHeaderDTO.BillingAddress.ZipCode,
        };

        var payment = new Payment
        {
            CardName = orderHeaderDTO.Payment.CardName,
            CardNumber = orderHeaderDTO.Payment.CardNumber,
            Expiration = orderHeaderDTO.Payment.Expiration,
            CVV = orderHeaderDTO.Payment.CVV,
        };


        var orderHeader = OrderHeader.Create(
            customerId: orderHeaderDTO.CustomerId,
            address: billingAddress,
            orderStatus: OrderStatus.Pending,
            payment: payment
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

        if (DoesExistCustomer && customer != null)
        {
            customer.NumberOfOrders++;
            customer.LastOrder = DateTime.Now;
            customer.SpentMoney += orderHeader.TotalPrice;
        }

        return orderHeader;
    }
}
