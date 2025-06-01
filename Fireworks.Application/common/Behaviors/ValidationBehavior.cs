using Ardalis.Result;
using FluentValidation;
using MediatR;
using System.Reflection;
using FluentValidation.Results;

namespace Fireworks.Application.common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // 如果没有验证器，直接继续管道
        if (!validators.Any())
            return await next(cancellationToken);

        // 执行验证
        var validationFailures = await GetValidationFailuresAsync(request, cancellationToken);
        if (validationFailures.Count == 0)
            return await next(cancellationToken);

        // 创建错误响应
        return CreateErrorResponse(validationFailures);
    }

    private async Task<List<ValidationFailure>> GetValidationFailuresAsync(
        TRequest request,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        return validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();
    }

    private TResponse CreateErrorResponse(List<ValidationFailure> failures)
    {
        var errorMessages = failures
            .Select(f => f.ErrorMessage)
            .Where(msg => !string.IsNullOrWhiteSpace(msg))
            .ToList();

        // 处理非泛型 Result
        if (!typeof(TResponse).IsGenericType)
        {
            var validationErrors = errorMessages
                .Select(msg => new ValidationError(msg))
                .ToList();

            return (TResponse)(object)Result.Invalid(validationErrors);
        }

        // 处理泛型 Result<T>
        try
        {
            var resultType = typeof(TResponse).GetGenericArguments()[0];
            var factoryMethod = typeof(Result)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(m => m.Name == nameof(Result.Invalid) && m.IsGenericMethod)?
                .MakeGenericMethod(resultType);

            if (factoryMethod == null)
                throw new InvalidOperationException("Could not find Result.Invalid method");

            var validationErrors = errorMessages
                .Select(msg => new ValidationError(msg))
                .ToArray();

            // 使用 Result.Invalid<T> 而不是工厂类
            var result = factoryMethod.Invoke(null, [validationErrors]);
            return (TResponse)result!;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                "Failed to create validation result. " +
                "Ensure your response type is compatible with Ardalis.Result", ex);
        }
    }
}