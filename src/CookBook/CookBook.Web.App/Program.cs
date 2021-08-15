using System;
using System.Net.Http;
using System.Threading.Tasks;
using CookBook.Web.BL;
using CookBook.Web.DAL;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Web.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<CookBook.Web.App.App>("app");
            Install(builder);
            await builder.Build().RunAsync();
        }

        private static void Install(WebAssemblyHostBuilder builder)
        {
            new WebDALInstaller().Install(builder.Services);
            new WebBLInstaller().Install(builder.Services);

            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddAutoMapper(typeof(WebBLInstaller));
        }
    }
}
