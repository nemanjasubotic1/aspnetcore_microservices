namespace Services.OrderingService.OrderingService.Domain.AbstractModels;
public interface IEntity
{
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }
}
