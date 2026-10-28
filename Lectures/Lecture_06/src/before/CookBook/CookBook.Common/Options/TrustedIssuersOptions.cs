namespace CookBook.Common.Options;

/// <summary>
/// The full collection of bearer-token issuers the API trusts. Always registered at startup;
/// <see cref="IdentityOptions.IsEnabled"/> only controls whether endpoints require authorization.
/// </summary>
public class TrustedIssuersOptions
{
    public IList<TrustedIssuerOptions> Issuers { get; set; } = [];
}
