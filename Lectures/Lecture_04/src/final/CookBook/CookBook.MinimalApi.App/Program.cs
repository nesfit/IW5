using System;
using AutoMapper;
using CookBook.Api.BL.Facades;
using CookBook.Api.BL.Installers;
using CookBook.Api.DAL.Common;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.EF.Installers;
using CookBook.Api.DAL.Memory.Installers;
using CookBook.Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag.Annotations;

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

var app = builder.Build();

var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();

app.UseCors();
app.UseHttpsRedirection();

app.MapGet("api/ingredient",
    [OpenApiOperation("IngredientGetAll")](IIngredientFacade ingredientFacade) => ingredientFacade.GetAll());
app.MapGet("api/ingredient/{id:guid}",
    [OpenApiOperation("IngredientGetById")](IIngredientFacade ingredientFacade, Guid id) =>
        ingredientFacade.GetById(id));
app.MapPost("api/ingredient",
    [OpenApiOperation("IngredientCreate")](IIngredientFacade ingredientFacade, IngredientDetailModel ingredient) =>
        ingredientFacade.Create(ingredient));
app.MapPost("api/ingredient/upsert",
    [OpenApiOperation("IngredientCreateOrUpdate")](IIngredientFacade ingredientFacade,
        IngredientDetailModel ingredient) => ingredientFacade.CreateOrUpdate(ingredient));
app.MapPut("api/ingredient",
    [OpenApiOperation("IngredientUpdate")](IIngredientFacade ingredientFacade, IngredientDetailModel ingredient) =>
        ingredientFacade.Update(ingredient));
app.MapDelete("api/ingredient/{id:guid}",
    [OpenApiOperation("IngredientDelete")](IIngredientFacade ingredientFacade, Guid id) => ingredientFacade.Delete(id));

app.MapGet("api/recipe", [OpenApiOperation("RecipeGetAll")](IRecipeFacade recipeFacade) => recipeFacade.GetAll());
app.MapGet("api/recipe/{id:guid}",
    [OpenApiOperation("RecipeGetById")](IRecipeFacade recipeFacade, Guid id) => recipeFacade.GetById(id));

app.MapPost("api/recipe",
    [ApiExplorerSettings(GroupName = "v3")] [OpenApiOperation("RecipeCreate")](IRecipeFacade recipeFacade,
        RecipeDetailModel recipe) => recipeFacade.Create(recipe));
app.MapPost("api/recipe/upsert",
    [OpenApiOperation("RecipeCreateOrUpdate")](IRecipeFacade recipeFacade, RecipeDetailModel recipe) =>
        recipeFacade.CreateOrUpdate(recipe));
app.MapPut("api/recipe",
    [OpenApiOperation("RecipeUpdate")](IRecipeFacade recipeFacade, RecipeDetailModel recipe) =>
        recipeFacade.Update(recipe));
app.MapDelete("api/recipe/{id:guid}",
    [OpenApiOperation("RecipeDelete")](IRecipeFacade recipeFacade, Guid id) => recipeFacade.Delete(id));

app.Run();
