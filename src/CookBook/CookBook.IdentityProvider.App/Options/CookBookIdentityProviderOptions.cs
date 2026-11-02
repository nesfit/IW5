using CookBook.Common.Options;

namespace CookBook.IdentityProvider.App.Options;

public class CookBookIdentityProviderOptions
{
    public LocalLoginOptions Local { get; set; } = new();
    public IList<ExternalProviderOptions> ExternalProviders { get; set; } = [];
}

public class LocalLoginOptions
{
    public bool IsEnabled { get; set; } = true;
}

public class ExternalProviderOptions
{
    public required string Scheme { get; set; }
    public required string DisplayName { get; set; }
    public AuthenticationProviderType ProviderType { get; set; }
    public bool IsEnabled { get; set; }
    public required string Authority { get; set; }
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public string CallbackPath { get; set; } = "/signin-entra";
    public string SignedOutCallbackPath { get; set; } = "/signout-callback-entra";
    public string RemoteSignOutPath { get; set; } = "/signout-entra";
    public bool UpstreamLogoutIsEnabled { get; set; } = true;
    public string SubjectClaimType { get; set; } = "oid";
    public string TenantClaimType { get; set; } = "tid";
    public string EmailClaimType { get; set; } = "email";
    public string EmailVerifiedClaimType { get; set; } = "email_verified";
    public string RoleClaimType { get; set; } = "roles";
}
