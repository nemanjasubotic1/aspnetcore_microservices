using FluentValidation;
using GeneralUsing.CQRS;
using MediatR;

namespace GeneralUsing.MediatorPipelineBehaviors;
public class FluentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public FluentValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(_validators.Select(l => l.ValidateAsync(context, cancellationToken)));

        var failures = validationResults.Where(l => l.Errors.Any()).SelectMany(l => l.Errors).ToList();

        if (failures.Any())
            throw new ValidationException(failures);

        return await next();
    }
}
