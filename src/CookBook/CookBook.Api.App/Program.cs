using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using CookBook.Api.App.Extensions;
using CookBook.Api.App.Processors;
using CookBook.Api.BL.Installers;
using CookBook.Api.DAL.Common;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.EF.Extensions;
using CookBook.Api.DAL.EF.Installers;
using CookBook.Api.DAL.Memory.Installers;
using CookBook.Common.Extensions;
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

var builder = WebApplication.CreateBuilder();

ConfigureControllers(builder.Services);
ConfigureOpenApiDocuments(builder.Services);
ConfigureDependencies(builder.Services, builder.Configuration);
ConfigureAutoMapper(builder.Services);

var app = builder.Build();

ValidateAutoMapperConfiguration(app.Services);
UseDevelopmentSettings(app);
UseSecurityFeatures(app);
UseLocalization(app);
UseRouting(app);
UseOpenApi(app);

app.Run();

void ConfigureControllers(IServiceCollection serviceCollection)
{
    serviceCollection.AddApiVersioning(options =>
    {
        options.ApiVersionReader = new QueryStringApiVersionReader("version");
        options.DefaultApiVersion = new ApiVersion(3, 0);
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
    });

    serviceCollection.AddControllers()
        .AddNewtonsoftJson()
        .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<ApiBLInstaller>())
        .AddDataAnnotationsLocalization();
    serviceCollection.AddLocalization(options => options.ResourcesPath = string.Empty);

    serviceCollection.AddCors(options =>
    {
        options.AddDefaultPolicy(options =>
            options.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
    });
}

void ConfigureOpenApiDocuments(IServiceCollection serviceCollection)
{
    serviceCollection.AddVersionedApiExplorer(options =>
    {
        options.AddApiVersionParametersWhenVersionNeutral = true;
    });

    serviceCollection.AddOpenApiDocument(document =>
    {
        document.Title = "CookBook API v1";
        document.DocumentName = "v1";
        document.ApiGroupNames = new[] { "1.0" };
    });
    serviceCollection.AddOpenApiDocument(document =>
    {
        document.Title = "CookBook API v2";
        document.DocumentName = "v2";
        document.ApiGroupNames = new[] { "2.0" };
    });
    serviceCollection.AddOpenApiDocument(document =>
    {
        document.Title = "CookBook API v3";
        document.DocumentName = "v3";
        document.ApiGroupNames = new[] { "3.0" };
        document.OperationProcessors.Add(new RequestCultureOperationProcessor());
    });
}

void ConfigureDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
{
    if (!Enum.TryParse<DALType>(configuration.GetSection("DALSelectionOptions")["Type"], out var dalType))
    {
        throw new ArgumentOutOfRangeException("DALSelectionOptions:Type");
    }

    switch (dalType)
    {
        case DALType.Memory:
            serviceCollection.AddInstaller<ApiDALMemoryInstaller>();
            break;
        case DALType.EntityFramework:
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            serviceCollection.AddInstaller<ApiDALEFInstaller>(connectionString);
            break;
    }

    serviceCollection.AddInstaller<ApiBLInstaller>();
}

void ConfigureAutoMapper(IServiceCollection serviceCollection)
{
    serviceCollection.AddAutoMapper(typeof(EntityBase), typeof(ApiBLInstaller));
}


void ValidateAutoMapperConfiguration(IServiceProvider serviceProvider)
{
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

void UseDevelopmentSettings(WebApplication application)
{
    var environment = application.Services.GetRequiredService<IWebHostEnvironment>();

    if (environment.IsDevelopment())
    {
        application.UseDeveloperExceptionPage();
    }
}

void UseSecurityFeatures(IApplicationBuilder application)
{
    application.UseCors();
    application.UseHttpsRedirection();
    application.UseAuthorization();
}

void UseLocalization(IApplicationBuilder application)
{
    application.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture(new CultureInfo("en")),
        SupportedCultures = new List<CultureInfo> { new("en"), new("cs") }
    });

    application.UseRequestCulture();
}

void UseRouting(IApplicationBuilder application)
{
    application.UseRouting();

    application.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}

void UseOpenApi(IApplicationBuilder application)
{
    application.UseOpenApi();
    application.UseSwaggerUi3(settings =>
    {
        settings.DocumentTitle = "CookBook Swagger UI";
        settings.SwaggerRoutes.Add(new SwaggerUi3Route("v3.0", "/swagger/v3/swagger.json"));
        settings.SwaggerRoutes.Add(new SwaggerUi3Route("v2.0", "/swagger/v2/swagger.json"));
        settings.SwaggerRoutes.Add(new SwaggerUi3Route("v1.0", "/swagger/v1/swagger.json"));
    });
}


// Make the implicit Program class public so test projects can access it
public partial class Program
{
}
