﻿using static BasketECommerce.Web.SD;

namespace BasketECommerce.Web.Models.Orders;

public class OrderHeaderDTO
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ShoppingCartId { get; set; }
    public CustomerDTO? Customer { get; set; }

    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string EmailAddress { get; set; }
    public string AddressLine { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }


    public string? CardName { get; set; }
    public string? CardNumber { get; set; }
    public string? Expiration { get; set; }
    public string? CVV { get; set; }

    public DateTime? CreatedAt { get; set; }
    public string? Status { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Discount { get; set; }
    public List<OrderDetailsDTO> OrderDetails { get; set; } = [];
}
