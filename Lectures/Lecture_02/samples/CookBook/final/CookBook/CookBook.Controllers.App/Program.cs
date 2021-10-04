using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using CookBook.Api.BL.Facades;
using CookBook.Api.BL.Installers;
using CookBook.Api.DAL.Common;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.EF.Installers;
using CookBook.Api.DAL.Memory.Installers;
using CookBook.Common.Models;
using CookBook.Controllers.App.Extensions;
using CookBook.Controllers.App.Processors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(EntityBase), typeof(ApiBLInstaller));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

Enum.TryParse<DALType>(builder.Configuration.GetSection("DALSelectionOptions")["Type"], out var dalType);

switch (dalType)
{
    case DALType.Memory:
        new ApiDALMemoryInstaller().Install(builder.Services);
        break;
    case DALType.EntityFramework:
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        new ApiDALEFInstaller().Install(builder.Services, connectionString);
        break;
    default:
        throw new ArgumentOutOfRangeException("DALSelectionOptions:Type");
}

new ApiBLInstaller().Install(builder.Services);

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.ApiVersionReader = new QueryStringApiVersionReader("version");
    options.DefaultApiVersion = new ApiVersion(3, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.AddApiVersionParametersWhenVersionNeutral = true;
});

builder.Services.AddOpenApiDocument(document =>
{
    document.DocumentName = "v1";
    document.ApiGroupNames = new[] { "1.0" };
});

builder.Services.AddOpenApiDocument(document =>
{
    document.DocumentName = "v2";
    document.ApiGroupNames = new[] { "2.0" };
});

builder.Services.AddOpenApiDocument(document =>
{
    document.DocumentName = "v3";
    document.ApiGroupNames = new[] { "3.0" };
    document.OperationProcessors.Add(new RequestCultureOperationProcessor());
});

var app = builder.Build();

var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();

app.UseCors();
app.UseHttpsRedirection();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(new CultureInfo("en")),
    SupportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en"),
        new CultureInfo("cs")
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
    settings.SwaggerRoutes.Add(new SwaggerUi3Route("v3.0", "/swagger/v3/swagger.json"));
    settings.SwaggerRoutes.Add(new SwaggerUi3Route("v2.0", "/swagger/v2/swagger.json"));
    settings.SwaggerRoutes.Add(new SwaggerUi3Route("v1.0", "/swagger/v1/swagger.json"));
});

app.Run();
