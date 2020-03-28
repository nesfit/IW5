using CookBook.BL.Common.Installers;
using CookBook.BL.Web.Installers;
using CookBook.BL.Web.Options;
using CookBook.Common.Extensions;
using CookBook.Web.MVC.Installers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;

namespace CookBook.Web.MVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddOptions();

            services.AddInstaller<BLCommonInstaller>();
            services.AddInstaller<BLWebInstaller>();
            services.AddInstaller<WebMVCInstaller>();

            services.Configure<ApiOptions>(Configuration.GetSection(nameof(ApiOptions)));
            services.Configure<WebOptions>(Configuration.GetSection(nameof(WebOptions)));

            var webOptions = Configuration.GetSection(nameof(WebOptions)).Get<WebOptions>();

            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en"),
                new CultureInfo("cs")
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(new CultureInfo(webOptions.DefaultCulture));
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, new CustomCultureProvider());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<WebOptions> webOptions)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRequestLocalization();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "culture-route", pattern: $"{{culture={webOptions.Value.DefaultCulture}}}/{{controller=Home}}/{{action=Index}}/{{id?}}");
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
