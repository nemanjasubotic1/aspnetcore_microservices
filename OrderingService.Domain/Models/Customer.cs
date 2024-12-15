using OrderingService.Domain.AbstractModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderingService.Domain.Models;
public class Customer : Entity
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public DateTime? FirstOrder { get; set; }
    public DateTime? LastOrder { get; set; }
    public int NumberOfOrders { get; set; }
    public decimal SpentMoney { get; set; }
}
