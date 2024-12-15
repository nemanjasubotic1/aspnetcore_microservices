namespace BasketECommerce.Web.Models.Orders;

public record ChangeOrderStatusRequest(Guid Id, string status);