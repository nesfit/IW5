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

builder.Services.AddApiVersioning(options =>
{
    options.ApiVersionReader = new QueryStringApiVersionReader("version");
    options.DefaultApiVersion = new ApiVersion(3, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
});
builder.Services.AddControllers()
    .AddNewtonsoftJson()
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<ApiBLInstaller>());
builder.Services.AddLocalization(options => options.ResourcesPath = string.Empty);
builder.Services.AddVersionedApiExplorer(options =>
{
    options.AddApiVersionParametersWhenVersionNeutral = true;
});
builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "CookBook API v1";
    document.DocumentName = "v1";
    document.ApiGroupNames = new[] { "1.0" };
});
builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "CookBook API v2";
    document.DocumentName = "v2";
    document.ApiGroupNames = new[] { "2.0" };
});
builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "CookBook API v3";
    document.DocumentName = "v3";
    document.ApiGroupNames = new[] { "3.0" };
    document.OperationProcessors.Add(new RequestCultureOperationProcessor());
});

Enum.TryParse<DALType>(builder.Configuration.GetSection("DALSelectionOptions")["Type"], out var dalType);

switch (dalType)
{
    case DALType.Memory:
        builder.Services.AddInstaller<ApiDALMemoryInstaller>();
        break;
    case DALType.EntityFramework:
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddInstaller<ApiDALEFInstaller>(connectionString);
        break;
    default:
        throw new ArgumentOutOfRangeException("DALSelectionOptions:Type");
}

builder.Services.AddInstaller<ApiBLInstaller>();
builder.Services.AddAutoMapper(typeof(EntityBase), typeof(ApiBLInstaller));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();

var environment = app.Services.GetRequiredService<IWebHostEnvironment>();

if (environment.IsDevelopment())
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
    settings.DocumentTitle = "CookBook Swagger UI";
    settings.SwaggerRoutes.Add(new SwaggerUi3Route("v3.0", "/swagger/v3/swagger.json"));
    settings.SwaggerRoutes.Add(new SwaggerUi3Route("v2.0", "/swagger/v2/swagger.json"));
    settings.SwaggerRoutes.Add(new SwaggerUi3Route("v1.0", "/swagger/v1/swagger.json"));
});

app.Run();
