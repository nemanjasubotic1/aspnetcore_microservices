using System.ComponentModel.DataAnnotations;

namespace Services.OrderingService.OrderingService.Domain.AbstractModels;
public abstract class Entity : IEntity
{
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }
}
