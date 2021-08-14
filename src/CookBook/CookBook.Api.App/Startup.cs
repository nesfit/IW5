using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using CookBook.Api.App.Extensions;
using CookBook.Api.App.Processors;
using CookBook.Api.BL.Installers;
using CookBook.Api.DAL.Installers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag.AspNetCore;

namespace CookBook.Api.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new QueryStringApiVersionReader("version");
                options.DefaultApiVersion = new ApiVersion(3, 0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<BLApiInstaller>());

            services.AddLocalization(options => options.ResourcesPath = string.Empty);

            services.AddVersionedApiExplorer(options =>
            {
                options.AddApiVersionParametersWhenVersionNeutral = true;
            });

            services.AddOpenApiDocument(document =>
            {
                document.Title = "CookBook API v1";
                document.DocumentName = "v1";
                document.ApiGroupNames = new[] { "1.0" };
            });

            services.AddOpenApiDocument(document =>
            {
                document.Title = "CookBook API v2";
                document.DocumentName = "v2";
                document.ApiGroupNames = new[] { "2.0" };
            });

            services.AddOpenApiDocument(document =>
            {
                document.Title = "CookBook API v3";
                document.DocumentName = "v3";
                document.ApiGroupNames = new[] { "3.0" };
                document.OperationProcessors.Add(new RequestCultureOperationProcessor());
            });

            new DALInstaller().Install(services);
            new BLApiInstaller().Install(services);

            services.AddAutoMapper(typeof(DALInstaller), typeof(BLApiInstaller));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper)
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("en")),
                SupportedCultures = new List<CultureInfo>
                {
                    new("en"),
                    new("cs")
                }
            });

            app.UseRequestCulture();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3(settings =>
            {
                settings.DocumentTitle = "Test";
                settings.SwaggerRoutes.Add(new SwaggerUi3Route("v3.0", "/swagger/v3/swagger.json"));
                settings.SwaggerRoutes.Add(new SwaggerUi3Route("v2.0", "/swagger/v2/swagger.json"));
                settings.SwaggerRoutes.Add(new SwaggerUi3Route("v1.0", "/swagger/v1/swagger.json"));
            });
        }
    }
}
