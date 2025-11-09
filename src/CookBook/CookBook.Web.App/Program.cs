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

var apiOptions = builder.Configuration.GetSection(nameof(ApiOptions)).Get<ApiOptions>();
if (apiOptions is null)
{
    throw new ArgumentNullException("Failed to load ApiOptions from configuration.");
}

var identityOptions = builder.Configuration.GetSection(nameof(IdentityOptions)).Get<IdentityOptions>();
if (identityOptions is null)
{
    throw new ArgumentNullException("Failed to load IdentityOptions from configuration.");
}

builder.Services.Configure<IdentityOptions>(builder.Configuration.GetSection(nameof(IdentityOptions)));
builder.Services.AddInstaller<WebDALInstaller>();
if (apiOptions != null)
{
    builder.Services.AddInstaller<WebBLInstaller>(identityOptions, apiOptions);
}

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Configure Mapperly mappers
builder.Services.AddScoped<IngredientMapper>();
builder.Services.AddScoped<RecipeMapper>();
builder.Services.AddScoped<IMapper<CookBook.Common.Models.IngredientDetailModel, CookBook.Common.Models.IngredientListModel>>(sp => sp.GetService<IngredientMapper>()!);
builder.Services.AddScoped<IMapper<CookBook.Common.Models.RecipeDetailModel, CookBook.Common.Models.RecipeListModel>>(sp => sp.GetService<RecipeMapper>()!);

builder.Services.AddValidatorsFromAssemblyContaining<IngredientDetailModel>();

builder.Services.AddLocalization();

if (identityOptions?.IsIdentityEnabled is true)
{
    builder.Services.AddOidcAuthentication(options =>
    {
        builder.Configuration.Bind(nameof(IdentityOptions), options.ProviderOptions);
    });
}

var localDbEnabledString = builder.Configuration.GetSection(nameof(LocalDbOptions))[nameof(LocalDbOptions.IsLocalDbEnabled)];
builder.Services.Configure<LocalDbOptions>(options =>
{
    options.IsLocalDbEnabled = !string.IsNullOrEmpty(localDbEnabledString) && bool.Parse(localDbEnabledString);
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
