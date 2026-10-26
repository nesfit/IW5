namespace CookBook.Api.App.Authorization;

/// <summary>
/// The API trusts several issuers, and their subject values may collide (for example a local username
/// equal to an Entra object ID). Owner identifiers are therefore qualified by the trusted issuer scheme.
/// </summary>
public static class ResourceOwnerClaims
{
    public const string OwnerIdClaimType = "cookbook_owner_id";

    public static string CreateOwnerId(string issuerScheme, string subject)
        => $"{issuerScheme}:{subject}";
}
