using AutoMapper.Internal;
using CookBook.Common.Extensions;
using CookBook.Web.App;
using CookBook.Web.BL.Extensions;
using CookBook.Web.BL.Installers;
using CookBook.Web.BL.Options;
using CookBook.Web.DAL.Installers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("appsettings.json");

var apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl");
if(apiBaseUrl is null)
{
    throw new ArgumentNullException(nameof(apiBaseUrl), "Missing ApiBaseUrl in configuration");
}

builder.Services.AddInstaller<WebDALInstaller>();
builder.Services.AddInstaller<WebBLInstaller>(apiBaseUrl);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAutoMapper(configuration =>
{
    // This is a temporary fix - should be able to remove this when version 11.0.2 comes out
    // More information here: https://github.com/AutoMapper/AutoMapper/issues/3988
    configuration.Internal().MethodMappingEnabled = false;
}, typeof(WebBLInstaller));
builder.Services.AddLocalization();

builder.Services.Configure<LocalDbOptions>(options =>
{
    var isLocalDbEnabled = builder.Configuration.GetSection(nameof(LocalDbOptions))[nameof(LocalDbOptions.IsLocalDbEnabled)];
    options.IsLocalDbEnabled = string.IsNullOrWhiteSpace(isLocalDbEnabled) is false && bool.Parse(isLocalDbEnabled);
});


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
