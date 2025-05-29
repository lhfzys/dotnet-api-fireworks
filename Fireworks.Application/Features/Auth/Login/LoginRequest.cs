using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;

namespace Fireworks.Application.Features.Auth.Login;

public class LoginRequest:IRequest<Result<AuthResponse>>
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    [JsonIgnore]
    public string IpAddress { get; set; } = null!;
}