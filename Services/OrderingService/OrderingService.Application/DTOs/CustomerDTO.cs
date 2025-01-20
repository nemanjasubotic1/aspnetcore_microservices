namespace Services.OrderingService.OrderingService.Application.DTOs;
public class CustomerDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public DateTime? FirstOrder { get; set; }
    public DateTime? LastOrder { get; set; }
    public int NumberOfOrders { get; set; }
    public decimal SpentMoney { get; set; }
}
