using CookBook.IdentityProvider.App.Endpoints;
using CookBook.IdentityProvider.App.Options;
using CookBook.IdentityProvider.App.Services;
using CookBook.IdentityProvider.BL.Facades;
using CookBook.IdentityProvider.BL.Models;
using Duende.IdentityServer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace CookBook.IdentityProvider.App
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();

            var identityProviderOptions = builder.Configuration
                .GetSection(nameof(CookBookIdentityProviderOptions))
                .Get<CookBookIdentityProviderOptions>()
                ?? new CookBookIdentityProviderOptions();

            ValidateIdentityProviderOptions(identityProviderOptions);
            builder.Services.Configure<CookBookIdentityProviderOptions>(
                builder.Configuration.GetSection(nameof(CookBookIdentityProviderOptions)));

            var identityServerBuilder = builder.Services.AddIdentityServer(options =>
                {
                    // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
                    options.EmitStaticAudienceClaim = true;
                })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

            // Choose one user-store/profile pair. This is intentionally adjacent so the teaching
            // sample can switch from in-memory test users to persistent ASP.NET Identity by
            // commenting one pair and uncommenting the other; login/callback code does not change.
            //
            // Option 1 - in-memory Duende TestUserStore:
            // identityServerBuilder.AddTestUsers(TestUsers.Users);
            // builder.Services.AddScoped<IExternalUserService, TestStoreExternalUserService>();
            //
            // Option 2 - persistent ASP.NET Identity:
            identityServerBuilder.AddProfileService<LocalAppUserProfileService>();
            builder.Services.AddScoped<IExternalUserService, PersistentExternalUserService>();

            foreach (var provider in identityProviderOptions.ExternalProviders.Where(provider => provider.IsEnabled))
            {
                switch (provider.ProviderType)
                {
                    case Common.Options.AuthenticationProviderType.Entra:
                        builder.Services.AddAuthentication()
                            .AddOpenIdConnect(provider.Scheme, provider.DisplayName, options =>
                            {
                                options.Authority = provider.Authority;
                                options.ClientId = provider.ClientId;
                                options.ClientSecret = provider.ClientSecret;
                                options.CallbackPath = provider.CallbackPath;
                                options.SignedOutCallbackPath = provider.SignedOutCallbackPath;
                                options.ResponseType = "code";
                                options.UsePkce = true;
                                options.SaveTokens = provider.UpstreamLogoutIsEnabled;
                                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                                options.SignOutScheme = IdentityConstants.ApplicationScheme;
                                options.Scope.Clear();
                                options.Scope.Add("openid");
                                options.Scope.Add("profile");
                                options.Scope.Add("email");
                                options.MapInboundClaims = false;
                            });
                        break;
                    default:
                        throw new InvalidOperationException(
                            $"CookBookIdentityProviderOptions external provider '{provider.Scheme}' uses unsupported ProviderType '{provider.ProviderType}'.");
                }
            }

            return builder.Build();
        }

        private static void ValidateIdentityProviderOptions(CookBookIdentityProviderOptions options)
        {
            var enabledProviders = options.ExternalProviders.Where(provider => provider.IsEnabled).ToList();
            if (!options.Local.IsEnabled && enabledProviders.Count == 0)
            {
                throw new InvalidOperationException(
                    "CookBookIdentityProviderOptions must enable Local:IsEnabled or at least one ExternalProviders entry.");
            }

            var duplicateSchemes = options.ExternalProviders
                .GroupBy(provider => provider.Scheme, StringComparer.Ordinal)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToList();
            if (duplicateSchemes.Count > 0)
            {
                throw new InvalidOperationException(
                    $"CookBookIdentityProviderOptions:ExternalProviders contains duplicate Scheme values: {string.Join(", ", duplicateSchemes)}.");
            }

            foreach (var provider in enabledProviders)
            {
                if (string.IsNullOrWhiteSpace(provider.Scheme)
                    || string.IsNullOrWhiteSpace(provider.DisplayName)
                    || string.IsNullOrWhiteSpace(provider.Authority)
                    || string.IsNullOrWhiteSpace(provider.ClientId)
                    || string.IsNullOrWhiteSpace(provider.ClientSecret))
                {
                    throw new InvalidOperationException(
                        $"Enabled CookBookIdentityProviderOptions external provider '{provider.Scheme}' is missing Scheme, DisplayName, Authority, ClientId, or ClientSecret.");
                }
            }
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            app.UseCors(policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            });

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthorization();
            app.MapRazorPages().RequireAuthorization();
            app.UseUserEndpoints();

            return app;
        }
    }
}
