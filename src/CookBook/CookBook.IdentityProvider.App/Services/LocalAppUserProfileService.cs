using System.Security.Claims;
using CookBook.IdentityProvider.BL.Facades;
using CookBook.IdentityProvider.BL.Models;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

namespace CookBook.IdentityProvider.App.Services;

public class LocalAppUserProfileService(
    IAppUserFacade appUserFacade,
    IAppUserClaimsFacade appUserClaimsFacade)
    : IProfileService
{
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var subjectId = context.Subject.GetSubjectId();

        AppUserDetailModel? user;

        if (Guid.TryParse(subjectId, out var id))
        {
            user = await appUserFacade.GetUserByIdAsync(id);
        }
        else
        {
            user = await appUserFacade.GetUserByUserNameAsync(subjectId);
        }

        if (user is not null)
        {
            var appUserClaims = await appUserClaimsFacade.GetAppUserClaimsByUserIdAsync(user.Id);
            var claims = appUserClaims.Select(claim =>
            {
                if (claim.ClaimType is not null
                    && claim.ClaimValue is not null)
                {
                    return new Claim(claim.ClaimType, claim.ClaimValue);
                }
                return null;
            }).ToList();

            if (user.UserName is not null)
            {
                claims.Add(new Claim("username", user.UserName));
            }
            context.AddRequestedClaims(claims);
        }
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;
        return Task.CompletedTask;
    }
}
