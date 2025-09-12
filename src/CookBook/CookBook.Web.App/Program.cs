using CookBook.Common.Extensions;
using CookBook.Web.App;
using CookBook.Web.BL.Extensions;
using CookBook.Web.BL.Installers;
using CookBook.Web.BL.Mapping;
using CookBook.Web.BL.Options;
using CookBook.Web.DAL.Installers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("appsettings.json");

var apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl");

builder.Services.AddInstaller<WebDALInstaller>();
builder.Services.AddInstaller<WebBLInstaller>(apiBaseUrl);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Configure Mapster mappings
WebMapsterConfig.RegisterMappings();

builder.Services.AddLocalization();

builder.Services.Configure<LocalDbOptions>(options =>
{
    options.IsLocalDbEnabled = bool.Parse(builder.Configuration.GetSection(nameof(LocalDbOptions))[nameof(LocalDbOptions.IsLocalDbEnabled)]);
});


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
