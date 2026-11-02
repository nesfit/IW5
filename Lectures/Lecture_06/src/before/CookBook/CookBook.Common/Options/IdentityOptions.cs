namespace CookBook.Common.Options;

/// <summary>
/// Shared authorization/UI switch. In the API this solely controls whether protected endpoints
/// require authorization (trusted token issuers are always registered, see <see cref="TrustedIssuersOptions"/>).
/// In the Web app it controls whether remote authentication (OIDC) is configured at all.
/// </summary>
public class IdentityOptions
{
    public bool IsEnabled { get; set; }
}
