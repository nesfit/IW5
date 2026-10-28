namespace CookBook.Common.Options;

/// <summary>
/// One authentication provider registration the Blazor WebAssembly client can be configured to use.
/// The Web app selects exactly one enabled registration by <see cref="WebAuthenticationOptions.SelectedProvider"/>;
/// each provider type is handled by its own adapter so that adding future providers (Microsoft, Google,
/// Facebook, ...) does not require changing this shape.
/// </summary>
public class WebAuthenticationProviderOptions
{
    public AuthenticationProviderType ProviderType { get; set; }
    public bool IsEnabled { get; set; }
    public required string Authority { get; set; }
    public required string ClientId { get; set; }
    public string ResponseType { get; set; } = "code";
    public string? RedirectUri { get; set; }
    public string? PostLogoutRedirectUri { get; set; }
    public IList<string> DefaultScopes { get; set; } = [];
}
