using System.Security.Claims;

namespace CookBook.IdentityProvider.App.Services;

public sealed record ExternalUser(
    string SubjectId,
    string DisplayName,
    IReadOnlyCollection<Claim> SessionClaims);

public interface IExternalUserService
{
    Task<ExternalUser> FindOrProvisionAsync(
        string provider,
        string providerSubject,
        string? verifiedEmail,
        ClaimsPrincipal externalPrincipal,
        CancellationToken cancellationToken);
}
