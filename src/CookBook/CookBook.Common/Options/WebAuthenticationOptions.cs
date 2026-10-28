namespace CookBook.Common.Options;

/// <summary>
/// Web authentication configuration: the provider the Blazor WebAssembly client is configured to use,
/// plus the full set of provider registrations it can choose from.
/// </summary>
public class WebAuthenticationOptions
{
    /// <summary>The provider that should be used for interactive login. Ignored when <see cref="IdentityOptions.IsEnabled"/> is false.</summary>
    public AuthenticationProviderType SelectedProvider { get; set; }

    public IList<WebAuthenticationProviderOptions> Providers { get; set; } = [];
}
