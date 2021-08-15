using System;
using System.Net.Http;
using CookBook.Web.BL.Installers;
using CookBook.Web.DAL.Installers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<CookBook.Web.App.App>("app");

var apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl");
new WebDALInstaller().Install(builder.Services);
new WebBLInstaller().Install(builder.Services, apiBaseUrl);
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAutoMapper(typeof(WebBLInstaller));

await builder.Build().RunAsync();
