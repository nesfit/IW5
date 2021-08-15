using System;
using CookBook.Api.BL.Facades;
using CookBook.Api.BL.Installers;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Memory.Installers;
using CookBook.Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NSwag.Annotations;

var builder = WebApplication.CreateBuilder(args);

new ApiDALMemoryInstaller().Install(builder.Services);
new ApiBLInstaller().Install(builder.Services);
builder.Services.AddAutoMapper(typeof(EntityBase), typeof(ApiBLInstaller));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors();
app.UseHttpsRedirection();

app.MapGet("api/ingredient", [OpenApiOperation("IngredientGetAll")](IngredientFacade ingredientFacade) => ingredientFacade.GetAll());
app.MapGet("api/ingredient/{id:guid}", [OpenApiOperation("IngredientGetById")](IngredientFacade ingredientFacade, Guid id) => ingredientFacade.GetById(id));
app.MapPost("api/ingredient", [OpenApiOperation("IngredientCreate")](IngredientFacade ingredientFacade, IngredientDetailModel ingredient) => ingredientFacade.Create(ingredient));
app.MapPost("api/ingredient/upsert", [OpenApiOperation("IngredientCreateOrUpdate")](IngredientFacade ingredientFacade, IngredientDetailModel ingredient) => ingredientFacade.CreateOrUpdate(ingredient));
app.MapPut("api/ingredient", [OpenApiOperation("IngredientUpdate")](IngredientFacade ingredientFacade, IngredientDetailModel ingredient) => ingredientFacade.Update(ingredient));
app.MapDelete("api/ingredient/{id:guid}", [OpenApiOperation("IngredientDelete")](IngredientFacade ingredientFacade, Guid id) => ingredientFacade.Delete(id));

app.MapGet("api/recipe", [OpenApiOperation("RecipeGetAll")](RecipeFacade recipeFacade) => recipeFacade.GetAll());
app.MapGet("api/recipe/{id:guid}", [OpenApiOperation("RecipeGetById")](RecipeFacade recipeFacade, Guid id) => recipeFacade.GetById(id));

app.MapPost("api/recipe", [OpenApiOperation("RecipeCreate")](RecipeFacade recipeFacade, RecipeDetailModel recipe) => recipeFacade.Create(recipe));
app.MapPost("api/recipe/upsert", [OpenApiOperation("RecipeCreateOrUpdate")](RecipeFacade recipeFacade, RecipeDetailModel recipe) => recipeFacade.CreateOrUpdate(recipe));
app.MapPut("api/recipe", [OpenApiOperation("RecipeUpdate")](RecipeFacade recipeFacade, RecipeDetailModel recipe) => recipeFacade.Update(recipe));
app.MapDelete("api/recipe/{id:guid}", [OpenApiOperation("RecipeDelete")](RecipeFacade recipeFacade, Guid id) => recipeFacade.Delete(id));

app.Run();
