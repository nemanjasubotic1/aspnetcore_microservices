using Services.OrderingService.OrderingService.Application.DTOs;
using Services.OrderingService.OrderingService.Domain;

namespace OrderingService.Application.DTOs;
public class OrderHeaderDTO
{
    internal IEnumerable<OrderDetailsDTO> a;

    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public CustomerDTO? Customer { get; set; }

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? EmailAddress { get; set; }
    public string? AddressLine { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }


    public string? CardName { get; set; }
    public string? CardNumber { get; set; }
    public string? Expiration { get; set; }
    public string? CVV { get; set; }

    public DateTime? CreatedAt { get; set; }
    public string Status { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderDetailsDTO> OrderDetails { get; set; } = [];

}
