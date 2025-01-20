using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasketECommerce.Web.Models.ProductCategory.ViewModels;

public class ProductVM
{
    public ProductModel ProductModel { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> CategoryItems { get; set; }
}
