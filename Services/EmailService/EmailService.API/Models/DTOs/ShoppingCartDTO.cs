﻿namespace Services.EmailService.EmailService.API.Models.DTOs;

public class ShoppingCartDTO
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public List<CartItemDTO> CartItems { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public decimal Discount { get; set; }
    public decimal CartTotal => CartItems.Sum(l => l.Price * l.Quantity);
}
