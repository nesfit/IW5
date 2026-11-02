using CookBook.Api.BL.Models;
using Microsoft.AspNetCore.Authorization;

namespace CookBook.Api.App.Authorization;

public class OwnerOrAdminAuthorizationHandler
    : AuthorizationHandler<OwnerOrAdminRequirement, ResourceOwnership>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OwnerOrAdminRequirement requirement,
        ResourceOwnership resource)
    {
        if (context.User.IsAdmin() || context.User.IsOwnerOf(resource.OwnerId))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
