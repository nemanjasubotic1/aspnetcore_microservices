using OrderingService.Application.DTOs;
using Services.OrderingService.OrderingService.Domain.Models;

namespace Services.OrderingService.OrderingService.Application.Extensions;
public static class OrderExtensions
{
    public static OrderHeaderDTO OrderToOrderDTO(this OrderHeader orderHeader)
    {
        var orderDto = new OrderHeaderDTO
        {
            Id = orderHeader.Id,
            CustomerId = orderHeader.CustomerId,

            FirstName = orderHeader.BillingAddress.FirstName,
            LastName = orderHeader.BillingAddress.LastName,
            EmailAddress = orderHeader.BillingAddress.EmailAddress,
            AddressLine = orderHeader.BillingAddress.AddressLine,
            Country = orderHeader.BillingAddress.Country,
            State = orderHeader.BillingAddress.State,
            ZipCode = orderHeader.BillingAddress.ZipCode,

            CardName = orderHeader.Payment.CardName,
            CardNumber = orderHeader.Payment.CardNumber,
            Expiration = orderHeader.Payment.Expiration,
            CVV = orderHeader.Payment.CVV,

            Status = orderHeader.Status.ToString(),
            TotalPrice = orderHeader.TotalPrice,
            Discount = orderHeader.Discount,    

            CreatedAt = orderHeader.CreatedAt,
        };

        orderDto.OrderDetails.AddRange(orderHeader.OrderDetails.Select(orderDetail => new OrderDetailsDTO
        {
            ProductId = orderDetail.ProductId,
            ProductName = orderDetail.ProductName,
            Quantity = orderDetail.Quantity,
            Price = orderDetail.Price,
        }));

        return orderDto;
    }


    public static IEnumerable<OrderHeaderDTO> OrdersListToOrdersDtoList(this IEnumerable<OrderHeader> orderHeaders)
    {
        return orderHeaders.Select(orderHeader => new OrderHeaderDTO
        {
            Id = orderHeader.Id,
            CustomerId = orderHeader.CustomerId,

            FirstName = orderHeader.BillingAddress.FirstName,
            LastName = orderHeader.BillingAddress.LastName,
            EmailAddress = orderHeader.BillingAddress.EmailAddress,
            AddressLine = orderHeader.BillingAddress.AddressLine,
            Country = orderHeader.BillingAddress.Country,
            State = orderHeader.BillingAddress.State,
            ZipCode = orderHeader.BillingAddress.ZipCode,

            CardName = orderHeader.Payment.CardName,
            CardNumber = orderHeader.Payment.CardNumber,
            Expiration = orderHeader.Payment.Expiration,
            CVV = orderHeader.Payment.CVV,

            Status = orderHeader.Status.ToString(),
            TotalPrice = orderHeader.TotalPrice,
            Discount = orderHeader.Discount,
            CreatedAt = orderHeader.CreatedAt,

        });
    }
}
