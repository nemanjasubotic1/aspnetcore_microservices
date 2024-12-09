﻿namespace ProductCategory.API.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price{ get; set; }
    public string? ImageUrl { get; set; }
    public DateOnly YearOfProduction { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}
