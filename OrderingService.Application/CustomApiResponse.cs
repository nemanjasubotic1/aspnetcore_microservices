using FluentValidation.Results;

namespace OrderingService.Application;

public record CustomApiResponse(object? Result = null, bool IsSuccess = true, List<ValidationFailure>? Errors = null);