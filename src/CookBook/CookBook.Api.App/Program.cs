using System.Globalization;
using CookBook.Api.App.Extensions;
using CookBook.Api.App.Processors;
using CookBook.Api.BL.Facades;
using CookBook.Api.BL.Installers;
using CookBook.Api.BL.Mappers;
using CookBook.Api.DAL.Common;
using CookBook.Api.DAL.EF.Extensions;
using CookBook.Api.DAL.EF.Installers;
using CookBook.Api.DAL.Memory.Installers;
using CookBook.Common.Extensions;
using CookBook.Common.Models;
using CookBook.Common.Resources;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder();

ConfigureCors(builder.Services);
ConfigureLocalization(builder.Services);
ConfigureValidation(builder.Services);

ConfigureOpenApiDocuments(builder.Services);
ConfigureDependencies(builder.Services, builder.Configuration);
ConfigureMapperly(builder.Services);

var app = builder.Build();

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

void ConfigureValidation(IServiceCollection serviceCollection)
{
    serviceCollection.AddScoped<IValidator<IngredientDetailModel>, IngredientDetailModelValidator>();
    serviceCollection.AddScoped<IValidator<RecipeDetailModel>, RecipeDetailModelValidator>();
    serviceCollection.AddScoped<IValidator<RecipeDetailIngredientModel>, RecipeDetailIngredientModelValidator>();
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

void ConfigureMapperly(IServiceCollection serviceCollection)
{
    serviceCollection.AddSingleton<IngredientMapper>();
    serviceCollection.AddSingleton<RecipeMapper>();
}

async Task<Dictionary<string, string[]>?> ValidateModelAsync<T>(T model, IValidator<T> validator)
{
    var validationResult = await validator.ValidateAsync(model);

    if (validationResult.IsValid)
    {
        return null;
    }

    return validationResult.Errors
        .GroupBy(e => e.PropertyName)
        .ToDictionary(
            g => g.Key,
            g => g.Select(e => e.ErrorMessage).ToArray()
        );
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

    ingredientEndpoints.MapPost("", async Task<Results<Ok<Guid>, ValidationProblem>> (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade, IValidator<IngredientDetailModel> validator) =>
    {
        var validationErrors = await ValidateModelAsync(ingredient, validator);
        if (validationErrors != null)
        {
            return TypedResults.ValidationProblem(validationErrors);
        }

        var id = ingredientFacade.Create(ingredient);
        return TypedResults.Ok(id);
    });

    ingredientEndpoints.MapPut("", async Task<Results<Ok<Guid?>, ValidationProblem>> (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade, IValidator<IngredientDetailModel> validator) =>
    {
        var validationErrors = await ValidateModelAsync(ingredient, validator);
        if (validationErrors != null)
        {
            return TypedResults.ValidationProblem(validationErrors);
        }

        var id = ingredientFacade.Update(ingredient);
        return TypedResults.Ok(id);
    });

    ingredientEndpoints.MapPost("upsert", async Task<Results<Ok<Guid>, ValidationProblem>> (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade, IValidator<IngredientDetailModel> validator) =>
    {
        var validationErrors = await ValidateModelAsync(ingredient, validator);
        if (validationErrors != null)
        {
            return TypedResults.ValidationProblem(validationErrors);
        }

        var id = ingredientFacade.CreateOrUpdate(ingredient);
        return TypedResults.Ok(id);
    });

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

    recipeEndpoints.MapPost("", async Task<Results<Ok<Guid>, ValidationProblem>> (RecipeDetailModel recipe, IRecipeFacade recipeFacade, IValidator<RecipeDetailModel> validator) =>
    {
        var validationErrors = await ValidateModelAsync(recipe, validator);
        if (validationErrors != null)
        {
            return TypedResults.ValidationProblem(validationErrors);
        }

        var id = recipeFacade.Create(recipe);
        return TypedResults.Ok(id);
    });

    recipeEndpoints.MapPut("", async Task<Results<Ok<Guid?>, ValidationProblem>> (RecipeDetailModel recipe, IRecipeFacade recipeFacade, IValidator<RecipeDetailModel> validator) =>
    {
        var validationErrors = await ValidateModelAsync(recipe, validator);
        if (validationErrors != null)
        {
            return TypedResults.ValidationProblem(validationErrors);
        }

        var id = recipeFacade.Update(recipe);
        return TypedResults.Ok(id);
    });

    recipeEndpoints.MapPost("upsert", async Task<Results<Ok<Guid>, ValidationProblem>> (RecipeDetailModel recipe, IRecipeFacade recipeFacade, IValidator<RecipeDetailModel> validator) =>
    {
        var validationErrors = await ValidateModelAsync(recipe, validator);
        if (validationErrors != null)
        {
            return TypedResults.ValidationProblem(validationErrors);
        }

        var id = recipeFacade.CreateOrUpdate(recipe);
        return TypedResults.Ok(id);
    });

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
