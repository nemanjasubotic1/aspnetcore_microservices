namespace OrderingService.Application.DTOs;
public class AddressDTO
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? EmailAddress { get; set; }
    public string? AddressLine { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
}
