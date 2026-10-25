using CookBook.Api.BL.Models;
using Microsoft.AspNetCore.Authorization;

namespace CookBook.Api.App.Authorization;

public class AdminOnlyAuthorizationHandler
    : AuthorizationHandler<AdminOnlyRequirement, ResourceOwnership>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AdminOnlyRequirement requirement,
        ResourceOwnership resource)
    {
        if (context.User.IsAdmin())
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
