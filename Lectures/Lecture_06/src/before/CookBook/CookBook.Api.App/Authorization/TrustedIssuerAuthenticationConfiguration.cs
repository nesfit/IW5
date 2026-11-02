using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CookBook.Common.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace CookBook.Api.App.Authorization;

public static class TrustedIssuerAuthenticationConfiguration
{
    public static void Configure(
        IServiceCollection serviceCollection,
        TrustedIssuersOptions trustedIssuersOptions,
        IdentityOptions identityOptions)
    {
        TrustedIssuerTokenValidation.ValidateOptions(trustedIssuersOptions, identityOptions);

        serviceCollection.AddAuthorization();
        serviceCollection.AddSingleton<IAuthorizationHandler, OwnerOrAdminAuthorizationHandler>();
        serviceCollection.AddSingleton<IAuthorizationHandler, AdminOnlyAuthorizationHandler>();
        serviceCollection.AddHttpContextAccessor();

        if (trustedIssuersOptions.Issuers.Count == 0)
        {
            serviceCollection.AddAuthentication();
            return;
        }

        const string policySchemeName = "TrustedIssuerSelector";

        var authenticationBuilder = serviceCollection.AddAuthentication(options =>
        {
            options.DefaultScheme = policySchemeName;
            options.DefaultChallengeScheme = policySchemeName;
        });

        authenticationBuilder.AddPolicyScheme(policySchemeName, "Selects a trusted issuer scheme by the token's iss claim", options =>
        {
            options.ForwardDefaultSelector = context => SelectSchemeByIssuer(context, trustedIssuersOptions.Issuers);
        });

        foreach (var issuer in trustedIssuersOptions.Issuers)
        {
            authenticationBuilder.AddJwtBearer(issuer.Scheme, options =>
            {
                options.Authority = issuer.Authority;
                options.MapInboundClaims = false;
                options.TokenValidationParameters.ValidateIssuer = true;
                options.TokenValidationParameters.ValidIssuers = issuer.ValidIssuers;
                options.TokenValidationParameters.ValidateAudience = true;
                options.TokenValidationParameters.ValidAudiences = issuer.ValidAudiences;
                options.TokenValidationParameters.ValidateLifetime = true;
                options.TokenValidationParameters.ValidateIssuerSigningKey = true;
                options.TokenValidationParameters.NameClaimType = issuer.NameClaimType;
                options.TokenValidationParameters.RoleClaimType = issuer.RoleClaimType;
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        if (context.Principal?.Identity is ClaimsIdentity identity
                            && TrustedIssuerTokenValidation.NormalizeClaims(identity, issuer) is { } failure)
                        {
                            context.Fail(failure);
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        }
    }

    private static string SelectSchemeByIssuer(HttpContext context, IList<TrustedIssuerOptions> issuers)
    {
        var authorizationHeader = context.Request.Headers.Authorization.ToString();
        if (!string.IsNullOrEmpty(authorizationHeader)
            && authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            var token = authorizationHeader["Bearer ".Length..].Trim();
            var handler = new JwtSecurityTokenHandler();
            if (handler.CanReadToken(token))
            {
                try
                {
                    var jwt = handler.ReadJwtToken(token);
                    var matchingIssuer = issuers.FirstOrDefault(issuer =>
                        issuer.ValidIssuers.Contains(jwt.Issuer, StringComparer.Ordinal));
                    if (matchingIssuer is not null)
                    {
                        return matchingIssuer.Scheme;
                    }
                }
                catch (ArgumentException)
                {
                    // The selected JWT handler returns the authentication failure.
                }
            }
        }

        return issuers[0].Scheme;
    }
}
