using GeneralUsing.Exceptions.CustomExceptionHandlers;

namespace ProductCategory.API.Exceptions;

public class CategoryNotFoundException : NotFoundException
{
    public CategoryNotFoundException(string message) : base(message)
    {
    }
}
