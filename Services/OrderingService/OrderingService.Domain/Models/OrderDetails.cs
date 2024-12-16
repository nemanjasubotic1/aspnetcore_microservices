using Services.OrderingService.OrderingService.Domain.AbstractModels;
using Services.OrderingService.OrderingService.Domain.Models;

namespace OrderingService.Domain.Models;
public class OrderDetails : Entity
{
    public Guid OrderHeaderId { get; set; }
    public OrderHeader OrderHeader { get; set; }

    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
