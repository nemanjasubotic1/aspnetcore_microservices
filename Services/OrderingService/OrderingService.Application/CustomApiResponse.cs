using FluentValidation.Results;

namespace Services.OrderingService.OrderingService.Application;

public record CustomApiResponse(object? Result = null, bool IsSuccess = true, List<ValidationFailure>? Errors = null);