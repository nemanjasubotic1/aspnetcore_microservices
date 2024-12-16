namespace Services.OrderingService.OrderingService.Application.DTOs;
public class PaymentDTO
{
    public string? CardName { get; set; }
    public string? CardNumber { get; set; }
    public string? Expiration { get; set; }
    public string? CVV { get; set; }
}
