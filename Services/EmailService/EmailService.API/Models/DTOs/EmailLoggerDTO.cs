using System.ComponentModel.DataAnnotations;

namespace Services.EmailService.EmailService.API.Models.DTOs;

public class EmailLoggerDTO
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string Discriminator { get; set; } = string.Empty;
}
