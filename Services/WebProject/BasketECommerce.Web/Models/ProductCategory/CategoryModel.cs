using FluentValidation.Results;

namespace BasketECommerce.Web.Models.ProductCategory;

public class CategoryModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string Description { get; set; } = default!;
}


