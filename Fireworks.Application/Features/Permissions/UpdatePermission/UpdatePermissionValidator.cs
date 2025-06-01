using FluentValidation;

namespace Fireworks.Application.Features.Permissions.UpdatePermission;

public class UpdatePermissionValidator : AbstractValidator<UpdatePermissionRequest>
{
    public UpdatePermissionValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        
        RuleFor(x => x)
            .Must(HasAtLeastOneUpdatableField)
            .WithMessage("至少需要提供一个要更新的字段（除 Id 之外）");
    }
    
    private bool HasAtLeastOneUpdatableField(UpdatePermissionRequest request)
    {
        return request.Name != null ||
               request.Code != null ||
               request.Type.HasValue ||
               request.ParentId.HasValue ||
               request.Url != null ||
               request.Icon != null ||
               request.Order.HasValue ||
               request.Description != null ||
               request.IsEnabled.HasValue;
    }
}
