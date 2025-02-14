using System.Security.Claims;
using CookBook.IdentityProvider.DAL.Entities;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;

namespace CookBook.IdentityProvider.App.Services;

public class LocalAppUserProfileService(
    UserManager<AppUserEntity> userManager)
    : IProfileService
{
    private readonly UserManager<AppUserEntity> userManager = userManager;

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var subjectId = context.Subject.GetSubjectId();
        var user = await userManager.FindByIdAsync(subjectId);
        if (user is not null)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            var userClaims = await userManager.GetClaimsAsync(user);
            context.AddRequestedClaims(userRoles.Select(role => new Claim("role", role)));
            context.AddRequestedClaims(userClaims);
        }
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;
        return Task.CompletedTask;
    }
}
