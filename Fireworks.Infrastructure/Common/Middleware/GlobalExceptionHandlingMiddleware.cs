using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fireworks.Infrastructure.Common.Middleware;

public class GlobalExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlingMiddleware> logger,
    IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "未处理的异常：{Message}", ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorMessage = env.IsDevelopment() ? ex.ToString() : "服务器内部错误，请联系管理员";

            var result = Result.Error(errorMessage);

            await context.Response.WriteAsJsonAsync(result);
        }
    }
}