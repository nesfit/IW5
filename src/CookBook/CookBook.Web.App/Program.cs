using CookBook.Common.Extensions;
using CookBook.Web.App;
using CookBook.Web.BL.Extensions;
using CookBook.Web.BL.Installers;
using CookBook.Web.BL.Mappers;
using CookBook.Web.BL.Options;
using CookBook.Web.DAL.Installers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("appsettings.json");

var apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl");

builder.Services.AddInstaller<WebDALInstaller>();
builder.Services.AddInstaller<WebBLInstaller>(apiBaseUrl);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Configure Mapperly mappers
builder.Services.AddScoped<IngredientMapper>();
builder.Services.AddScoped<RecipeMapper>();
builder.Services.AddScoped<IMapper<CookBook.Common.Models.IngredientDetailModel, CookBook.Common.Models.IngredientListModel>>(sp => sp.GetService<IngredientMapper>()!);
builder.Services.AddScoped<IMapper<CookBook.Common.Models.RecipeDetailModel, CookBook.Common.Models.RecipeListModel>>(sp => sp.GetService<RecipeMapper>()!);

builder.Services.AddLocalization();

builder.Services.Configure<LocalDbOptions>(options =>
{
    options.IsLocalDbEnabled = bool.Parse(builder.Configuration.GetSection(nameof(LocalDbOptions))[nameof(LocalDbOptions.IsLocalDbEnabled)]);
});


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

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
