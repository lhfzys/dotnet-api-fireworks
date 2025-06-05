using Ardalis.Result;
using Fireworks.Application.common.Models;
using Fireworks.Application.common.Requests;
using MediatR;

namespace Fireworks.Application.Features.Users.GetAllUsers;

public class GetUsersRequest : PaginationRequest, IRequest<Result<PaginatedResponse<UserResponse>>>
{
    public string? Username { get; set; }
}