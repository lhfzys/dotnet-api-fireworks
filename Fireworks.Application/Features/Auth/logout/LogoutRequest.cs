using Ardalis.Result;
using MediatR;

namespace Fireworks.Application.Features.Auth.logout;

public class LogoutRequest:IRequest<Result>
{
    public string RefreshToken { get; set; } = null!;
}