using System.Security.Claims;
using CookBook.Common;
using CookBook.IdentityProvider.App.Options;
using CookBook.IdentityProvider.App.Services;
using Duende.IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace CookBook.IdentityProvider.App.Pages.ExternalLogin;

[AllowAnonymous]
[SecurityHeaders]
public class Callback(
    IIdentityServerInteractionService interaction,
    IEventService events,
    ILogger<Callback> logger,
    IExternalUserService users,
    IOptions<CookBookIdentityProviderOptions> options) : PageModel
{
    public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
    {
        var result = await HttpContext.AuthenticateAsync(
            IdentityServerConstants.ExternalCookieAuthenticationScheme);
        if (result.Succeeded != true || result.Principal is null)
        {
            logger.LogWarning("External authentication callback did not contain a successful external cookie.");
            return RedirectToPage("/Account/Login/Index");
        }

        if (!result.Properties.Items.TryGetValue("scheme", out var scheme)
            || string.IsNullOrWhiteSpace(scheme))
        {
            logger.LogWarning("External authentication callback did not contain a provider scheme.");
            return BadRequest("External authentication state is invalid.");
        }

        var providerOptions = options.Value.ExternalProviders.SingleOrDefault(provider =>
            provider.IsEnabled && string.Equals(provider.Scheme, scheme, StringComparison.Ordinal));
        if (providerOptions is null)
        {
            logger.LogWarning("External authentication callback referenced unknown scheme {Scheme}.", scheme);
            return BadRequest("External authentication provider is not enabled.");
        }

        var principal = result.Principal;
        var providerSubject = GetProviderSubject(principal, providerOptions);
        var verifiedEmail = GetVerifiedEmail(principal, providerOptions);
        var user = await users.FindOrProvisionAsync(
            scheme,
            providerSubject,
            verifiedEmail,
            principal,
            cancellationToken);

        var additionalClaims = BuildSessionClaims(principal, providerOptions, user.SessionClaims);
        var signInProperties = new AuthenticationProperties();
        CaptureExternalLoginContext(result, providerOptions, additionalClaims, signInProperties);

        await HttpContext.SignInAsync(
            new IdentityServerUser(user.SubjectId)
            {
                DisplayName = user.DisplayName,
                IdentityProvider = scheme,
                AdditionalClaims = additionalClaims
            },
            signInProperties);

        await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);

        var returnUrl = result.Properties.Items.TryGetValue("returnUrl", out var storedReturnUrl)
            ? storedReturnUrl ?? "~/"
            : "~/";
        if (!Url.IsLocalUrl(returnUrl) && !interaction.IsValidReturnUrl(returnUrl))
        {
            logger.LogWarning("External authentication callback contained an invalid return URL.");
            return BadRequest("External authentication return URL is invalid.");
        }

        var context = await interaction.GetAuthorizationContextAsync(returnUrl, cancellationToken);
        await events.RaiseAsync(
            new UserLoginSuccessEvent(
                scheme,
                providerSubject,
                user.SubjectId,
                user.DisplayName,
                true,
                context?.Client.ClientId),
            cancellationToken);

        return context?.IsNativeClient() == true
            ? this.LoadingPage(returnUrl)
            : Redirect(returnUrl);
    }

    private static string GetProviderSubject(
        ClaimsPrincipal principal,
        ExternalProviderOptions provider)
    {
        var subject = principal.FindFirst(provider.SubjectClaimType)?.Value
            ?? principal.FindFirst(JwtClaimTypes.Subject)?.Value
            ?? principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new InvalidOperationException("External provider did not issue a stable subject claim.");

        var tenant = principal.FindFirst(provider.TenantClaimType)?.Value;
        return string.IsNullOrWhiteSpace(tenant) ? subject : $"{tenant}:{subject}";
    }

    private static string? GetVerifiedEmail(
        ClaimsPrincipal principal,
        ExternalProviderOptions provider)
    {
        var email = principal.FindFirst(provider.EmailClaimType)?.Value;
        var verified = principal.FindFirst(provider.EmailVerifiedClaimType)?.Value;
        return !string.IsNullOrWhiteSpace(email)
               && bool.TryParse(verified, out var isVerified)
               && isVerified
            ? email
            : null;
    }

    private static List<Claim> BuildSessionClaims(
        ClaimsPrincipal principal,
        ExternalProviderOptions provider,
        IReadOnlyCollection<Claim> userClaims)
    {
        var knownRoles = new HashSet<string>(
            [UserRoles.User, UserRoles.Admin],
            StringComparer.Ordinal);

        return principal.FindAll(provider.RoleClaimType)
            .Concat(userClaims.Where(claim => claim.Type is "role" or "roles" or ClaimTypes.Role))
            .Select(claim => claim.Value)
            .Where(knownRoles.Contains)
            .Distinct(StringComparer.Ordinal)
            .Select(role => new Claim(JwtClaimTypes.Role, role))
            .ToList();
    }

    private static void CaptureExternalLoginContext(
        AuthenticateResult externalResult,
        ExternalProviderOptions provider,
        ICollection<Claim> localClaims,
        AuthenticationProperties localSignInProperties)
    {
        var sessionId = externalResult.Principal?.FindFirst(JwtClaimTypes.SessionId);
        if (sessionId is not null)
        {
            localClaims.Add(new Claim(JwtClaimTypes.SessionId, sessionId.Value));
        }

        if (!provider.UpstreamLogoutIsEnabled)
        {
            return;
        }

        var idToken = externalResult.Properties?.GetTokenValue("id_token");
        if (!string.IsNullOrWhiteSpace(idToken))
        {
            localSignInProperties.StoreTokens(
                [new AuthenticationToken { Name = "id_token", Value = idToken }]);
        }
    }
}
