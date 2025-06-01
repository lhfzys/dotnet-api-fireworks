using Ardalis.Result;
using FluentValidation.Results;

namespace Fireworks.Application.common.Factories;

public  static class ResultFactory
{
    public static Result<T> FromValidationFailures<T>(List<ValidationFailure> failures)
    {
        var validationErrors = failures
            .Select(f => new ValidationError(f.PropertyName, f.ErrorMessage))
            .ToList();

        return Result<T>.Invalid(validationErrors);
    }
}