namespace BasketECommerce.Web.Models.Orders;

public record CreateOrderRequest(OrderHeaderDTO OrderHeaderDTO, CustomerDTO CustomerDTO);
