namespace BasketECommerce.Web.Models.Orders;

public class OrderDetailsDTO
{
    public Guid OrderHeaderId { get; set; }
    public OrderHeaderDTO OrderHeader { get; set; }

    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
