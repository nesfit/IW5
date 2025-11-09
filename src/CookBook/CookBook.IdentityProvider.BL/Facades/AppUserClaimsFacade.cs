using System.Security.Claims;
using CookBook.IdentityProvider.BL.Mappers;
using CookBook.IdentityProvider.BL.Models;
using CookBook.IdentityProvider.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace CookBook.IdentityProvider.BL.Facades;

public class AppUserClaimsFacade(
    UserManager<AppUserEntity> userManager,
    AppUserClaimMapper appUserClaimMapper)
    : IAppUserClaimsFacade
{
    public async Task<IEnumerable<AppUserClaimListModel>> GetAppUserClaimsByUserIdAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return new List<AppUserClaimListModel>();
        }
        else
        {
            var claims = await userManager.GetClaimsAsync(user);

            var roles = await userManager.GetRolesAsync(user);
            claims = [.. claims, .. roles.Select(role => new Claim("role", role))];

            return claims.Select(appUserClaimMapper.ToListModel);
        }
    }
}
