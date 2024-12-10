using System.ComponentModel.DataAnnotations;

namespace EmailService.API.Models;

public class EmailLogger
{
    [Key]
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string Discriminator { get; set; } = string.Empty;
}
