using System.Globalization;
using AutoMapper;
using CookBook.Api.App;
using CookBook.Api.App.Endpoints;
using CookBook.Api.App.Extensions;
using CookBook.Api.App.Installers;
using CookBook.Api.App.Processors;
using CookBook.Api.BL.Installers;
using CookBook.Api.DAL.Common;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.EF.Extensions;
using CookBook.Api.DAL.EF.Installers;
using CookBook.Api.DAL.Memory.Installers;
using CookBook.Common;
using CookBook.Common.Extensions;
using CookBook.Common.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using NSwag;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder();
var identityOptions = builder.Configuration.GetSection(nameof(IdentityOptions)).Get<IdentityOptions>()
    ?? new()
    {
        IsEnabled = false,
        IdentityServerUrl = string.Empty
    };

ConfigureCors(builder.Services);
ConfigureLocalization(builder.Services);

ConfigureOpenApiDocuments(builder.Services, identityOptions);
ConfigureDependencies(builder.Services, builder.Configuration);
ConfigureAutoMapper(builder.Services);
if (identityOptions.IsEnabled)
{
    ConfigureAuthentication(builder.Services, identityOptions.IdentityServerUrl);
}

var app = builder.Build();

ValidateAutoMapperConfiguration(app.Services);

UseDevelopmentSettings(app);
UseSecurityFeatures(app);
UseLocalization(app);
UseRouting(app);
if (identityOptions.IsEnabled)
{
    UseAuthorization(app);
}
UseEndpoints(app, identityOptions);
UseOpenApi(app);

app.Run();

void ConfigureCors(IServiceCollection serviceCollection)
{
    serviceCollection.AddCors(options =>
    {
        options.AddDefaultPolicy(o =>
            o.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
    });
}

void ConfigureLocalization(IServiceCollection serviceCollection)
{
    serviceCollection.AddLocalization(options => options.ResourcesPath = string.Empty);
}

void ConfigureOpenApiDocuments(IServiceCollection serviceCollection, IdentityOptions identityOptions)
{
    serviceCollection.AddEndpointsApiExplorer();
    serviceCollection.AddOpenApiDocument(settings =>
    {
        if(identityOptions.IsEnabled)
        {
            settings.AddSecurity(JwtBearerDefaults.AuthenticationScheme,
            [],
            new OpenApiSecurityScheme()
            {
                Type = OpenApiSecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    ClientCredentials = new OpenApiOAuthFlow
                    {
                        Scopes = new Dictionary<string, string>{
                          { "cookbookapi", "CookBook API access" },
                        },
                        TokenUrl = $"{identityOptions.IdentityServerUrl}/connect/token"
                    }
                }
            });
        }

        settings.OperationProcessors.Add(new RequestCultureOperationProcessor());
    });
}

void ConfigureDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
{
    if (!Enum.TryParse<DALType>(configuration.GetSection("DALSelectionOptions")["Type"], out var dalType))
    {
        throw new ArgumentException("DALSelectionOptions:Type");
    }

    switch (dalType)
    {
        case DALType.Memory:
            serviceCollection.AddInstaller<ApiDALMemoryInstaller>();
            break;
        case DALType.EntityFramework:
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentException("The connection string is missing");
            serviceCollection.AddInstaller<ApiDALEFInstaller>(connectionString);
            break;
    }

    serviceCollection.AddInstaller<ApiBLInstaller>();
    serviceCollection.AddInstaller<ApiAppInstaller>();
}

void ConfigureAutoMapper(IServiceCollection serviceCollection)
{
    serviceCollection.AddAutoMapper(typeof(EntityBase), typeof(ApiBLInstaller));
}

void ConfigureAuthentication(IServiceCollection serviceCollection, string identityServerUrl)
{
    serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = identityServerUrl;
            options.TokenValidationParameters.ValidateAudience = false;
        });

    serviceCollection.AddAuthorization(
        options =>
        {
            options.AddPolicy(ApiPolicies.IngredientAdmin, policy => policy.RequireRole(AppRoles.Admin));
        });
}

void ValidateAutoMapperConfiguration(IServiceProvider serviceProvider)
{
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

void UseEndpoints(WebApplication application, IdentityOptions identityOptions)
{
    var endpointsBase = application.MapGroup("api")
        .WithOpenApi();

    endpointsBase.UseIngredientEndpoints(identityOptions);
    endpointsBase.UseRecipeEndpoints(identityOptions);
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
}

void UseAuthorization(WebApplication application)
{
    application.UseAuthentication();
    application.UseAuthorization();
}

void UseOpenApi(WebApplication application)
{
    application.UseOpenApi(
        //options => options.Path = "openapi/{documentName}.json"
        );
    application.MapScalarApiReference();
    application.UseSwaggerUi();
}

// Make the implicit Program class public so test projects can access it
public partial class Program
{
}
