using CookBook.BL.Web.Blazor.Installers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookBook.Web.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            var blWebBlazorInstaller = new BLWebBlazorInstaller();
            blWebBlazorInstaller.Install(builder.Services);

            await builder.Build().RunAsync();
        }
    }
}
