using System;
using System.Net.Http;
using System.Threading.Tasks;
using CookBook.BL.Web;
using CookBook.DAL.Web;
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
            new DALWebInstaller().Install(builder.Services);
            new BLWebInstaller().Install(builder.Services);

            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddAutoMapper(typeof(BLWebInstaller));
        }
    }
}
