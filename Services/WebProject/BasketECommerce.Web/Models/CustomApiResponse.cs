using FluentValidation.Results;

namespace BasketECommerce.Web.Models;

public record CustomApiResponse(object? Result = null, bool IsSuccess = true, List<ValidationFailure>? Errors = null);


