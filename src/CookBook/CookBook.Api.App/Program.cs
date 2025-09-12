using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using AutoMapper.Internal;
using CookBook.Api.App.Extensions;
using CookBook.Api.App.Processors;
using CookBook.Api.BL.Facades;
using CookBook.Api.BL.Installers;
using CookBook.Api.DAL.Common;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.EF.Extensions;
using CookBook.Api.DAL.EF.Installers;
using CookBook.Api.DAL.Memory.Installers;
using CookBook.Common.Extensions;
using CookBook.Common.Models;
using CookBook.Common.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder();

ConfigureCors(builder.Services);
ConfigureLocalization(builder.Services);

ConfigureOpenApiDocuments(builder.Services);
ConfigureDependencies(builder.Services, builder.Configuration);
ConfigureAutoMapper(builder.Services);

var app = builder.Build();

ValidateAutoMapperConfiguration(app.Services);

UseDevelopmentSettings(app);
UseSecurityFeatures(app);
UseLocalization(app);
UseRouting(app);
UseEndpoints(app);
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

void ConfigureOpenApiDocuments(IServiceCollection serviceCollection)
{
    serviceCollection.AddEndpointsApiExplorer();
    serviceCollection.AddOpenApiDocument(settings =>
        settings.OperationProcessors.Add(new RequestCultureOperationProcessor()));
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
}

void ConfigureAutoMapper(IServiceCollection serviceCollection)
{
    serviceCollection.AddAutoMapper(cfg => { }, typeof(EntityBase).Assembly, typeof(ApiBLInstaller).Assembly);
}

void ValidateAutoMapperConfiguration(IServiceProvider serviceProvider)
{
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

void UseEndpoints(WebApplication application)
{
    var endpointsBase = application.MapGroup("api")
        .WithOpenApi();

    UseIngredientEndpoints(endpointsBase);
    UseRecipeEndpoints(endpointsBase);
}

void UseIngredientEndpoints(RouteGroupBuilder routeGroupBuilder)
{
    var ingredientEndpoints = routeGroupBuilder.MapGroup("ingredient")
        .WithTags("ingredient");

    ingredientEndpoints.MapGet("", (IIngredientFacade ingredientFacade) => ingredientFacade.GetAll());

    ingredientEndpoints.MapGet("{id:guid}", Results<Ok<IngredientDetailModel>, NotFound<string>> (Guid id, IIngredientFacade ingredientFacade, IStringLocalizer<IngredientEndpointsResources> ingredientEndpointsLocalizer)
        => ingredientFacade.GetById(id) is { } ingredient
            ? TypedResults.Ok(ingredient)
            : TypedResults.NotFound(ingredientEndpointsLocalizer[nameof(IngredientEndpointsResources.GetById_NotFound), id].Value));

    ingredientEndpoints.MapPost("", (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade) => ingredientFacade.Create(ingredient));
    ingredientEndpoints.MapPut("", (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade) => ingredientFacade.Update(ingredient));
    ingredientEndpoints.MapPost("upsert", (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade) => ingredientFacade.CreateOrUpdate(ingredient));
    ingredientEndpoints.MapDelete("{id:guid}", (Guid id, IIngredientFacade ingredientFacade) => ingredientFacade.Delete(id));
}

void UseRecipeEndpoints(RouteGroupBuilder routeGroupBuilder)
{
    var recipeEndpoints = routeGroupBuilder.MapGroup("recipe")
        .WithTags("recipe");

    recipeEndpoints.MapGet("", (IRecipeFacade recipeFacade) => recipeFacade.GetAll());

    recipeEndpoints.MapGet("{id:guid}", Results<Ok<RecipeDetailModel>, NotFound<string>> (Guid id, IRecipeFacade recipeFacade, IStringLocalizer<RecipeEndpointsResources> recipeEndpointsLocalizer)
        => recipeFacade.GetById(id) is { } recipe
            ? TypedResults.Ok(recipe)
            : TypedResults.NotFound(recipeEndpointsLocalizer[nameof(RecipeEndpointsResources.GetById_NotFound), id].Value));

    recipeEndpoints.MapPost("", (RecipeDetailModel recipe, IRecipeFacade recipeFacade) => recipeFacade.Create(recipe));
    recipeEndpoints.MapPut("", (RecipeDetailModel recipe, IRecipeFacade recipeFacade) => recipeFacade.Update(recipe));
    recipeEndpoints.MapPost("upsert", (RecipeDetailModel recipe, IRecipeFacade recipeFacade) => recipeFacade.CreateOrUpdate(recipe));
    recipeEndpoints.MapDelete("{id:guid}", (Guid id, IRecipeFacade recipeFacade) => recipeFacade.Delete(id));
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

void UseOpenApi(IApplicationBuilder application)
{
    application.UseOpenApi();
    application.UseSwaggerUi();
}


// Make the implicit Program class public so test projects can access it
public partial class Program
{
}
