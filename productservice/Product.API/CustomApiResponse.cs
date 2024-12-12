using FluentValidation.Results;

namespace ProductCategory.API;

public record CustomApiResponse(object? Result = null, bool IsSuccess = true, List<ValidationFailure>? Errors = null);


