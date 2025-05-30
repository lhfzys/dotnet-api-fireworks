using Ardalis.Result;
using Fireworks.Domain.Identity.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Fireworks.Application.Features.Users.GetUserById;

public class GetUserByIdHandler(UserManager<ApplicationUser> userManager)
    : IRequestHandler<GetUserByIdRequest, Result<UserResponse>>
{
    public async Task<Result<UserResponse>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        return user is null
            ? Result.Invalid(new ValidationError(nameof(request.Id), "用户不存在"))
            : Result.Success(user.Adapt<UserResponse>());
    }
}