﻿namespace OrderingService.Domain.ValueModels;
public class Payment
{
    public string? CardName { get; set; } 
    public string? CardNumber { get; set; } 
    public string? Expiration { get; set; }
    public string? CVV { get; set; } 
}
