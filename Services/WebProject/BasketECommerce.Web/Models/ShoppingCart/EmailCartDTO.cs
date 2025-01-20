namespace BasketECommerce.Web.Models.ShoppingCart;

public class EmailCartDTO
{
    public string UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateTime EmailSendAt { get; set; }
    public decimal CartTotal { get; set; }
}
