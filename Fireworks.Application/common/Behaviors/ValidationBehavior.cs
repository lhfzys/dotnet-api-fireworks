using Ardalis.Result;
using Fireworks.Application.common.Factories;
using FluentValidation;
using MediatR;

namespace Fireworks.Application.common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any()) return await next(cancellationToken);

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();
        if (failures.Count == 0) return await next(cancellationToken);

        var genericType = typeof(TResponse).GetGenericArguments()[0];
        var method = typeof(ResultFactory)
            .GetMethod(nameof(ResultFactory.FromValidationFailures))!
            .MakeGenericMethod(genericType);

        var result = method.Invoke(null, [failures]);
        return (TResponse)result!;
    }
}