using Ardalis.Result;

namespace Fireworks.Application.common;

public  static class ResultFactory
{
    public static Result<T> FromValidationFailures<T>(List<FluentValidation.Results.ValidationFailure> failures)
    {
        var validationErrors = failures
            .Select(f => new ValidationError(f.PropertyName, f.ErrorMessage))
            .ToList();

        return Result<T>.Invalid(validationErrors);
    }
}