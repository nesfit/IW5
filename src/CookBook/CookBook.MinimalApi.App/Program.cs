using CookBook.Api.BL.Facades;
using CookBook.Api.BL.Installers;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Memory.Installers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NSwag.Annotations;

var builder = WebApplication.CreateBuilder(args);

new ApiDALMemoryInstaller().Install(builder.Services);
new ApiBLInstaller().Install(builder.Services);
builder.Services.AddAutoMapper(typeof(EntityBase), typeof(ApiBLInstaller));

var app = builder.Build();

app.MapGet("api/ingredient", [OpenApiOperation("IngredientGetAll")](IngredientFacade ingredientFacade) => ingredientFacade.GetAll());

app.UseHttpsRedirection();

app.Run();
