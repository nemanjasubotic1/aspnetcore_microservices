using FluentValidation.Results;

namespace Main.ProductService.ProductCategory.API.InitialData;

public record CustomApiResponse(object? Result = null, bool IsSuccess = true, List<ValidationFailure>? Errors = null);


