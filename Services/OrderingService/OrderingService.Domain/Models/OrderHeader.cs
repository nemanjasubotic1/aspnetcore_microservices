﻿using OrderingService.Domain.Models;
using Services.OrderingService.OrderingService.Domain.AbstractModels;
using Services.OrderingService.OrderingService.Domain.EventModels;
using Services.OrderingService.OrderingService.Domain.ValueModels;

namespace Services.OrderingService.OrderingService.Domain.Models;
public class OrderHeader : Aggregate
{
    public Guid CustomerId { get; set; }

    public Address BillingAddress { get; set; }
    public Payment Payment { get; set; }    

    public OrderStatus Status { get; set; } = OrderStatus.Draft;

    public List<OrderDetails> OrderDetails { get; set; } = [];

    public decimal TotalPrice { get; set; }
    public decimal Discount { get; set; }

    public static OrderHeader Create(Guid customerId, Address address, OrderStatus orderStatus, Payment payment)
    {
        var orderHeader = new OrderHeader
        {
            CustomerId = customerId,
            BillingAddress = address,
            Status = orderStatus,
            Payment = payment,
        };

        orderHeader.AddDomainEvent(new OrderCreatedDomainEvent(orderHeader));

        return orderHeader;
    }

}
