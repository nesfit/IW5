namespace CookBook.Common.Options;

/// <summary>
/// One trusted bearer-token issuer the API validates against. The API registers one JWT bearer
/// authentication scheme per entry, regardless of <see cref="IdentityOptions.IsEnabled"/>: turning
/// authorization off only stops requiring authorization on endpoints, it never removes or loosens
/// token validation.
/// </summary>
public class TrustedIssuerOptions
{
    /// <summary>Unique authentication scheme name used to register and select this issuer's JWT bearer handler.</summary>
    public required string Scheme { get; set; }

    /// <summary>Provider type this issuer belongs to (used only for logging/diagnostics, not for validation).</summary>
    public AuthenticationProviderType ProviderType { get; set; }

    /// <summary>OIDC authority/metadata address used to resolve signing keys.</summary>
    public required string Authority { get; set; }

    /// <summary>Issuer values (`iss`) this scheme accepts. Must not be empty.</summary>
    public IList<string> ValidIssuers { get; set; } = [];

    /// <summary>Audience values (`aud`) this scheme accepts. Must not be empty; audience validation is always enabled.</summary>
    public IList<string> ValidAudiences { get; set; } = [];

    /// <summary>Claim type mapped to the ASP.NET Core name/subject claim (e.g. "sub" or Entra's "oid").</summary>
    public string NameClaimType { get; set; } = "sub";

    /// <summary>Claim type mapped to the ASP.NET Core role claim (e.g. "role" or Entra's "roles").</summary>
    public string RoleClaimType { get; set; } = "role";

    /// <summary>
    /// Delegated scopes (`scp`/`scope`) of which a token must contain at least one unless it carries an
    /// application role (e.g. Entra's "access_as_user"). Empty disables the check for other providers;
    /// Entra still requires "access_as_user" or an application role.
    /// </summary>
    public IList<string> RequiredScopes { get; set; } = [];
}
