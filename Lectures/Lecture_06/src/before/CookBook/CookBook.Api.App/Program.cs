using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CookBook.Api.App;
using CookBook.Api.App.Authorization;
using CookBook.Api.App.Extensions;
using CookBook.Api.App.Processors;
using CookBook.Api.BL.Installers;
using CookBook.Api.BL.Mappers;
using CookBook.Api.DAL.Common;
using CookBook.Api.DAL.EF.Extensions;
using CookBook.Api.DAL.EF.Installers;
using CookBook.Api.DAL.Memory.Installers;
using CookBook.Common;
using CookBook.Common.Extensions;
using CookBook.Common.Models;
using CookBook.Common.Options;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder();

ConfigureOptions(builder.Services);
ConfigureCors(builder.Services);
ConfigureLocalization(builder.Services);
ConfigureValidation(builder.Services);

ConfigureOpenApiDocuments(builder.Services);
ConfigureDependencies(builder.Services, builder.Configuration);
ConfigureMapperly(builder.Services);
ConfigureAuthentication(builder.Services, builder.Configuration);

var app = builder.Build();

UseDevelopmentSettings(app);
UseSecurityFeatures(app);
UseLocalization(app);
UseRouting(app);
UseAuthenticationAndAuthorization(app);
UseEndpoints(app);
UseOpenApi(app);

app.Run();

void ConfigureOptions(IServiceCollection serviceCollection)
{
    serviceCollection.Configure<IdentityOptions>(builder.Configuration.GetSection(nameof(IdentityOptions)));
}

void ConfigureCors(IServiceCollection serviceCollection)
{
    serviceCollection.AddCors(options =>
    {
        options.AddDefaultPolicy(o =>
            o.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
    });
}

void ConfigureLocalization(IServiceCollection serviceCollection)
{
    serviceCollection.AddLocalization(options => options.ResourcesPath = string.Empty);
}

void ConfigureValidation(IServiceCollection serviceCollection)
{
    serviceCollection.AddScoped<IValidator<IngredientDetailModel>, IngredientDetailModelValidator>();
    serviceCollection.AddScoped<IValidator<RecipeDetailModel>, RecipeDetailModelValidator>();
    serviceCollection.AddScoped<IValidator<RecipeDetailIngredientModel>, RecipeDetailIngredientModelValidator>();
}

void ConfigureOpenApiDocuments(IServiceCollection serviceCollection)
{
    serviceCollection.AddEndpointsApiExplorer();
    serviceCollection.AddOpenApiDocument(settings =>
        settings.OperationProcessors.Add(new RequestCultureOperationProcessor()));
}

void ConfigureDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
{
    if (!Enum.TryParse<DALType>(configuration.GetSection("DALSelectionOptions")["Type"], out var dalType))
    {
        throw new ArgumentException("DALSelectionOptions:Type");
    }

    switch (dalType)
    {
        case DALType.Memory:
            serviceCollection.AddInstaller<ApiDALMemoryInstaller>();
            break;
        case DALType.EntityFramework:
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentException("The connection string is missing");
            serviceCollection.AddInstaller<ApiDALEFInstaller>(connectionString);
            break;
    }

    serviceCollection.AddInstaller<ApiBLInstaller>();
}

void ConfigureMapperly(IServiceCollection serviceCollection)
{
    serviceCollection.AddSingleton<IngredientMapper>();
    serviceCollection.AddSingleton<RecipeMapper>();
}

void ConfigureAuthentication(IServiceCollection serviceCollection, IConfiguration configuration)
{
    // IdentityOptions.IsEnabled only controls whether protected endpoints call RequireAuthorization()
    // (see UseEndpoints/IngredientEndpoints/RecipeEndpoints below). Every trusted issuer below is always
    // registered and strictly validated, regardless of that switch, so toggling the demonstration
    // authorization requirement never rebuilds or loosens token validation.
    var trustedIssuersOptions = configuration.GetSection(nameof(TrustedIssuersOptions)).Get<TrustedIssuersOptions>()
        ?? new TrustedIssuersOptions();
    var identityOptions = configuration.GetSection(nameof(IdentityOptions)).Get<IdentityOptions>()
        ?? new IdentityOptions();

    ValidateTrustedIssuers(trustedIssuersOptions, identityOptions);

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

            // Disable ASP.NET Core's legacy inbound claim renaming so claim types stay exactly as
            // configured; normalization to ClaimTypes.NameIdentifier/ClaimTypes.Role below is then
            // explicit and identical for every current and future issuer.
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
                OnTokenValidated = context => NormalizeIssuerClaims(context, issuer)
            };
        });
    }

    serviceCollection.AddAuthorizationBuilder()
        .AddPolicy(ApiPolicies.IngredientAdmin, policy =>
            policy.RequireAuthenticatedUser()
            .RequireRole(UserRoles.Admin)
            )
        .AddPolicy(ApiPolicies.RecipeAdmin, policy =>
            policy.RequireAuthenticatedUser().RequireRole(UserRoles.Admin));

    serviceCollection.AddSingleton<IAuthorizationHandler, OwnerOrAdminAuthorizationHandler>();
    serviceCollection.AddSingleton<IAuthorizationHandler, AdminOnlyAuthorizationHandler>();
    serviceCollection.AddHttpContextAccessor();
}

void ValidateTrustedIssuers(
    TrustedIssuersOptions trustedIssuersOptions,
    IdentityOptions identityOptions)
{
    if (identityOptions.IsEnabled && trustedIssuersOptions.Issuers.Count == 0)
    {
        throw new InvalidOperationException(
            "TrustedIssuersOptions:Issuers must contain at least one entry when IdentityOptions:IsEnabled is true.");
    }

    foreach (var issuer in trustedIssuersOptions.Issuers)
    {
        if (string.IsNullOrWhiteSpace(issuer.Scheme)
            || string.IsNullOrWhiteSpace(issuer.Authority)
            || issuer.ValidIssuers.Count == 0
            || issuer.ValidAudiences.Count == 0
            || string.IsNullOrWhiteSpace(issuer.NameClaimType)
            || string.IsNullOrWhiteSpace(issuer.RoleClaimType))
        {
            throw new InvalidOperationException(
                $"TrustedIssuersOptions entry for provider '{issuer.ProviderType}' is missing Scheme, Authority, ValidIssuers, ValidAudiences, NameClaimType, or RoleClaimType.");
        }
    }

    var duplicateSchemes = trustedIssuersOptions.Issuers
        .GroupBy(i => i.Scheme, StringComparer.Ordinal)
        .Where(g => g.Count() > 1)
        .Select(g => g.Key)
        .ToList();

    if (duplicateSchemes.Count > 0)
    {
        throw new InvalidOperationException(
            $"TrustedIssuersOptions contains duplicate Scheme values: {string.Join(", ", duplicateSchemes)}");
    }
}

string SelectSchemeByIssuer(HttpContext context, IList<TrustedIssuerOptions> issuers)
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
                var matchingIssuer = issuers.FirstOrDefault(i =>
                    i.ValidIssuers.Contains(jwt.Issuer, StringComparer.Ordinal));
                if (matchingIssuer is not null)
                {
                    return matchingIssuer.Scheme;
                }
            }
            catch (ArgumentException)
            {
                // The selected real JWT handler returns the authentication failure.
            }
        }
    }

    // No configured issuer matches the token's unvalidated `iss` (or no bearer token is present).
    // Forward to any registered scheme so authentication fails closed with 401 rather than silently
    // succeeding or picking an unrelated issuer's validated identity.
    return issuers.Count > 0 ? issuers[0].Scheme : JwtBearerDefaults.AuthenticationScheme;
}

Task NormalizeIssuerClaims(Microsoft.AspNetCore.Authentication.JwtBearer.TokenValidatedContext context, TrustedIssuerOptions issuer)
{
    if (context.Principal?.Identity is not ClaimsIdentity identity)
    {
        return Task.CompletedTask;
    }

    var nameClaim = identity.FindFirst(issuer.NameClaimType);
    if (nameClaim is not null)
    {
        foreach (var existing in identity.FindAll(ClaimTypes.NameIdentifier).ToList())
        {
            identity.RemoveClaim(existing);
        }

        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, nameClaim.Value));
    }

    foreach (var roleClaim in identity.FindAll(issuer.RoleClaimType).ToList())
    {
        if (!identity.HasClaim(ClaimTypes.Role, roleClaim.Value))
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, roleClaim.Value));
        }
    }

    return Task.CompletedTask;
}

void UseAuthenticationAndAuthorization(IApplicationBuilder application)
{
    application.UseAuthentication();
    application.UseAuthorization();
}

void UseEndpoints(WebApplication application)
{
    var endpointsBase = application.MapGroup("api")
        .WithOpenApi();

    var identityOptions = application.Services.GetRequiredService<IOptions<IdentityOptions>>();

    endpointsBase.UseIngredientEndpoints(identityOptions);
    endpointsBase.UseRecipeEndpoints(identityOptions);
}

void UseDevelopmentSettings(WebApplication application)
{
    var environment = application.Services.GetRequiredService<IWebHostEnvironment>();

    if (environment.IsDevelopment())
    {
        application.UseDeveloperExceptionPage();
    }
}

void UseSecurityFeatures(IApplicationBuilder application)
{
    application.UseCors();
    application.UseHttpsRedirection();
}

void UseLocalization(IApplicationBuilder application)
{
    application.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture(new CultureInfo("en")),
        SupportedCultures = [new("en"), new("cs")]
    });

    application.UseRequestCulture();
}

void UseRouting(IApplicationBuilder application)
{
    application.UseRouting();
}

void UseOpenApi(IApplicationBuilder application)
{
    application.UseOpenApi();
    application.UseSwaggerUi();
}

// Make the implicit Program class public so test projects can access it
public partial class Program
{
}
