using Ardalis.Result;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Fireworks.Api.Extensions;

public static class MinimalApiResultExtensions
{
    public static IResult ToCustomMinimalApiResult<T>(this Result<T> result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(new
            {
                status = "Ok",
                value = result.Value,
                errors = result.Errors,
                validationErrors = new Dictionary<string, string[]>()
            }),

            ResultStatus.Invalid => Results.BadRequest(new
            {
                status = "Invalid",
                value = default(T),
                errors = result.Errors,
                validationErrors = result.ValidationErrors
                    .GroupBy(e => e.Identifier ?? "_no_identifier_")
                    .Where(g => !string.IsNullOrEmpty(g.Key))
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    )
            }),

            ResultStatus.Error => Results.BadRequest(new
            {
                status = "Error",
                value = default(T),
                errors = result.Errors,
                validationErrors = new Dictionary<string, string[]>()
            }),

            ResultStatus.NotFound => Results.NotFound(new
            {
                status = "NotFound",
                value = default(T),
                errors = result.Errors,
                validationErrors = new Dictionary<string, string[]>()
            }),

            ResultStatus.Forbidden => Results.Forbid(),
            ResultStatus.Unauthorized => Results.Unauthorized(),

            _ => Results.BadRequest()
        };
    }
}