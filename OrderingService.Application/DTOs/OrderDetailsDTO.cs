using OrderingService.Domain.Models;

namespace OrderingService.Application.DTOs;
public class OrderDetailsDTO
{
    public Guid Id { get; set; }
    public Guid OrderHeaderId { get; set; }
    public OrderHeader OrderHeader { get; set; }

    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
