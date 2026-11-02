using CookBook.IdentityProvider.App.Options;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace CookBook.IdentityProvider.App.Pages.ExternalLogin;

[AllowAnonymous]
[SecurityHeaders]
public class Challenge : PageModel
{
    private readonly IIdentityServerInteractionService _interactionService;
    private readonly CookBookIdentityProviderOptions _options;

    public Challenge(
        IIdentityServerInteractionService interactionService,
        IOptions<CookBookIdentityProviderOptions> options)
    {
        _interactionService = interactionService;
        _options = options.Value;
    }
        
    public IActionResult OnGet(string scheme, string returnUrl)
    {
        if (!_options.ExternalProviders.Any(provider =>
                provider.IsEnabled && string.Equals(provider.Scheme, scheme, StringComparison.Ordinal)))
        {
            return BadRequest("Unknown or disabled external authentication scheme.");
        }

        if (string.IsNullOrEmpty(returnUrl)) returnUrl = "~/";

        // validate returnUrl - either it is a valid OIDC URL or back to a local page
        if (Url.IsLocalUrl(returnUrl) == false && _interactionService.IsValidReturnUrl(returnUrl) == false)
        {
            // user might have clicked on a malicious link - should be logged
            throw new Exception("invalid return URL");
        }
            
        // start challenge and roundtrip the return URL and scheme 
        var props = new AuthenticationProperties
        {
            RedirectUri = Url.Page("/externallogin/callback"),
                
            Items =
            {
                { "returnUrl", returnUrl }, 
                { "scheme", scheme },
            }
        };

        return Challenge(props, scheme);
    }
}