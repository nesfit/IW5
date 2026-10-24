using System.Security.Claims;
using CookBook.IdentityProvider.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CookBook.IdentityProvider.App.Services;

public sealed class PersistentExternalUserService(
    UserManager<AppUserEntity> userManager,
    ILookupNormalizer normalizer,
    ILogger<PersistentExternalUserService> logger) : IExternalUserService
{
    public async Task<ExternalUser> FindOrProvisionAsync(
        string provider,
        string providerSubject,
        string? verifiedEmail,
        ClaimsPrincipal externalPrincipal,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByLoginAsync(provider, providerSubject);
        if (user is null && verifiedEmail is not null)
        {
            var normalizedEmail = normalizer.NormalizeEmail(verifiedEmail);
            var matches = await userManager.Users
                .Where(candidate => candidate.NormalizedEmail == normalizedEmail)
                .Take(2)
                .ToListAsync(cancellationToken);

            if (matches.Count > 1)
            {
                throw new InvalidOperationException(
                    "External login cannot be linked because more than one local account has the verified email.");
            }

            user = matches.SingleOrDefault();
            if (user is not null)
            {
                var linkResult = await userManager.AddLoginAsync(
                    user,
                    new UserLoginInfo(provider, providerSubject, provider));
                EnsureSucceeded(linkResult, "link the external login to the existing local account");
            }
        }

        if (user is null)
        {
            user = new AppUserEntity
            {
                Id = Guid.NewGuid(),
                Active = true,
                Subject = Guid.NewGuid().ToString(),
                Email = verifiedEmail,
                EmailConfirmed = verifiedEmail is not null,
                UserName = await CreateUniqueUserNameAsync(verifiedEmail, provider, providerSubject)
            };

            var createResult = await userManager.CreateAsync(user);
            EnsureSucceeded(createResult, "create the external shadow account");

            var linkResult = await userManager.AddLoginAsync(
                user,
                new UserLoginInfo(provider, providerSubject, provider));
            if (!linkResult.Succeeded)
            {
                var deleteResult = await userManager.DeleteAsync(user);
                if (!deleteResult.Succeeded)
                {
                    logger.LogError(
                        "Failed to clean up external shadow user {UserId} after login-link failure.",
                        user.Id);
                }

                EnsureSucceeded(linkResult, "link the external login to the new shadow account");
            }
        }

        if (!user.Active)
        {
            throw new InvalidOperationException("The linked local account is inactive.");
        }

        var roles = await userManager.GetRolesAsync(user);
        var sessionClaims = externalPrincipal.Claims
            .Where(claim => claim.Type is "role" or "roles")
            .Concat(roles.Select(role => new Claim("role", role)))
            .ToList();

        return new ExternalUser(
            user.Subject,
            user.UserName ?? verifiedEmail ?? user.Subject,
            sessionClaims);
    }

    private async Task<string> CreateUniqueUserNameAsync(
        string? verifiedEmail,
        string provider,
        string providerSubject)
    {
        var baseName = verifiedEmail?.Split('@')[0];
        if (string.IsNullOrWhiteSpace(baseName))
        {
            baseName = $"{provider}-{providerSubject}";
        }

        baseName = new string(baseName.Where(character => char.IsLetterOrDigit(character) || character is '-' or '_').ToArray());
        if (string.IsNullOrWhiteSpace(baseName))
        {
            baseName = "external-user";
        }

        var candidate = baseName;
        for (var suffix = 1; await userManager.FindByNameAsync(candidate) is not null; suffix++)
        {
            candidate = $"{baseName}-{suffix}";
        }

        return candidate;
    }

    private static void EnsureSucceeded(IdentityResult result, string operation)
    {
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                $"Failed to {operation}: {string.Join("; ", result.Errors.Select(error => error.Description))}");
        }
    }
}
