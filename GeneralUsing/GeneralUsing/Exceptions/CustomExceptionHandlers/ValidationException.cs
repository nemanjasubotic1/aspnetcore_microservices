namespace GeneralUsing.Exceptions.CustomExceptionHandlers;
public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }
}
