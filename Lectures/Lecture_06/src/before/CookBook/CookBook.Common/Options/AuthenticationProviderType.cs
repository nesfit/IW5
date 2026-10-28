namespace CookBook.Common.Options;

/// <summary>
/// Provider-neutral identifier for an authentication provider that can issue tokens consumed by CookBook.
/// New providers (Microsoft, Google, Facebook, ...) should add a new value here plus their own adapter,
/// without changing the shape of <see cref="WebAuthenticationProviderOptions"/> or <see cref="TrustedIssuerOptions"/>.
/// </summary>
public enum AuthenticationProviderType
{
    /// <summary>The CookBook IdentityProvider (Duende IdentityServer), supporting local and/or federated login.</summary>
    CookBookIdentityProvider,

    /// <summary>Direct, single-tenant Microsoft Entra ID authentication.</summary>
    Entra
}
