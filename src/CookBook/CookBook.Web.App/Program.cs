using System.Globalization;
using CookBook.Common.Extensions;
using CookBook.Common.Models;
using CookBook.Common.Options;
using CookBook.Web.App;
using CookBook.Web.App.Options;
using CookBook.Web.BL.Extensions;
using CookBook.Web.BL.Installers;
using CookBook.Web.BL.Mappers;
using CookBook.Web.BL.Options;
using CookBook.Web.DAL.Installers;
using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true);

var apiOptions = builder.Configuration.GetSection(nameof(ApiOptions)).Get<ApiOptions>();
if (apiOptions is null)
{
    throw new ArgumentNullException("Failed to load ApiOptions from configuration.");
}

var identityOptions = builder.Configuration.GetSection(nameof(IdentityOptions)).Get<IdentityOptions>()
    ?? throw new ArgumentNullException("Failed to load IdentityOptions from configuration.");

var webAuthenticationOptions = builder.Configuration.GetSection(nameof(WebAuthenticationOptions)).Get<WebAuthenticationOptions>()
    ?? new WebAuthenticationOptions();

WebAuthenticationProviderOptions? selectedProvider = null;
if (identityOptions.IsEnabled)
{
    selectedProvider = webAuthenticationOptions.Providers
        .FirstOrDefault(p => p.ProviderType == webAuthenticationOptions.SelectedProvider && p.IsEnabled);

    if (selectedProvider is null)
    {
        throw new InvalidOperationException(
            $"WebAuthenticationOptions:SelectedProvider is '{webAuthenticationOptions.SelectedProvider}', " +
            "but no enabled provider registration with that ProviderType was found under WebAuthenticationOptions:Providers.");
    }

    if (string.IsNullOrWhiteSpace(selectedProvider.Authority)
        || string.IsNullOrWhiteSpace(selectedProvider.ClientId)
        || selectedProvider.DefaultScopes.Count == 0)
    {
        throw new InvalidOperationException(
            $"The selected WebAuthenticationOptions provider '{selectedProvider.ProviderType}' is missing Authority, ClientId, or DefaultScopes.");
    }
}

builder.Services.Configure<IdentityOptions>(builder.Configuration.GetSection(nameof(IdentityOptions)));
builder.Services.Configure<WebAuthenticationOptions>(
    builder.Configuration.GetSection(nameof(WebAuthenticationOptions)));
builder.Services.AddInstaller<WebDALInstaller>();
if (apiOptions != null)
{
    builder.Services.AddInstaller<WebBLInstaller>(identityOptions, selectedProvider, apiOptions);
}

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Configure Mapperly mappers
builder.Services.AddScoped<IngredientMapper>();
builder.Services.AddScoped<RecipeMapper>();
builder.Services.AddScoped<IMapper<CookBook.Common.Models.IngredientDetailModel, CookBook.Common.Models.IngredientListModel>>(sp => sp.GetService<IngredientMapper>()!);
builder.Services.AddScoped<IMapper<CookBook.Common.Models.RecipeDetailModel, CookBook.Common.Models.RecipeListModel>>(sp => sp.GetService<RecipeMapper>()!);

builder.Services.AddValidatorsFromAssemblyContaining<IngredientDetailModel>();

builder.Services.AddLocalization();

if (identityOptions.IsEnabled && selectedProvider is not null)
{
    // Only the selected, enabled provider's registration is bound. Adding a future provider
    // (Microsoft, Google, Facebook, ...) means adding a new AuthenticationProviderType plus its own
    // adapter branch here; it does not change WebAuthenticationOptions itself.
    builder.Services.AddOidcAuthentication(options =>
    {
        options.ProviderOptions.Authority = selectedProvider.Authority;
        options.ProviderOptions.ClientId = selectedProvider.ClientId;
        options.ProviderOptions.ResponseType = selectedProvider.ResponseType;

        if (!string.IsNullOrWhiteSpace(selectedProvider.RedirectUri))
        {
            options.ProviderOptions.RedirectUri = selectedProvider.RedirectUri;
        }

        if (!string.IsNullOrWhiteSpace(selectedProvider.PostLogoutRedirectUri))
        {
            options.ProviderOptions.PostLogoutRedirectUri = selectedProvider.PostLogoutRedirectUri;
        }

        options.ProviderOptions.DefaultScopes.Clear();
        foreach (var scope in selectedProvider.DefaultScopes)
        {
            options.ProviderOptions.DefaultScopes.Add(scope);
        }
    });
}

var localDbEnabledString = builder.Configuration.GetSection(nameof(LocalDbOptions))[nameof(LocalDbOptions.IsEnabled)];
builder.Services.Configure<LocalDbOptions>(options =>
{
    options.IsEnabled = !string.IsNullOrEmpty(localDbEnabledString) && bool.Parse(localDbEnabledString);
});

var host = builder.Build();

// Initialize culture from localStorage
var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();
var jsInProcessRuntime = (IJSInProcessRuntime)jsRuntime;

try
{
    var culture = jsInProcessRuntime.Invoke<string>("language.get");
    if (!string.IsNullOrEmpty(culture))
    {
        var cultureInfo = new CultureInfo(culture);
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
    }
}
catch
{
    // If there's any error reading from localStorage, use default culture
    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
}

await host.RunAsync();
