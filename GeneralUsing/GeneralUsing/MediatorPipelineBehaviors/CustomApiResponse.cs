using FluentValidation.Results;

namespace GeneralUsing.MediatorPipelineBehaviors;

public record CustomApiResponse(object? Result = null, bool IsSuccess = true, List<ValidationFailure>? Errors = null);



