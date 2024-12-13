using OrderingService.Domain.Models;

namespace OrderingService.Application.DTOs;
public class CustomerDTO
{
    public Guid Id{ get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; }
    //public string FirstName { get; set; }
    //public string LastName { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public DateTime? FirstOrder { get; set; }
    public DateTime? LastOrder { get; set; }
    public int NumberOfOrders { get; set; }
    public decimal SpentMoney { get; set; }
    public List<OrderHeaderDTO> OrderHeaders { get; set; }
}
