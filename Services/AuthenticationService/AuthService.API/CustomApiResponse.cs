using FluentValidation.Results;

namespace AuthService.API;

public record CustomApiResponse(object? Result = null, bool IsSuccess = true, List<ValidationFailure>? Errors = null);
