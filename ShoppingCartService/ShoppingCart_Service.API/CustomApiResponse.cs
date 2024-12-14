using FluentValidation.Results;

namespace ShoppingCart_Service.API;

public record CustomApiResponse(object? Result = null, bool IsSuccess = true, List<ValidationFailure>? Errors = null);


