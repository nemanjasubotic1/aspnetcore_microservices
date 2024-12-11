using OrderingService.Domain;

namespace OrderingService.Application.DTOs;
public class OrderHeaderDTO
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public CustomerDTO Customer { get; set; }
    public AddressDTO BillingAddress { get; set; }
    public PaymentDTO Payment { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Draft;
    public decimal TotalPrice { get; set; }
    public List<OrderDetailsDTO> OrderDetails { get; set; } = [];

}
