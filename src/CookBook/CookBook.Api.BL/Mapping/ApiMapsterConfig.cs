using Mapster;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Mapping
{
    public static class ApiMapsterConfig
    {
        public static void RegisterMappings()
        {
            // Configure mappings for Ingredients
            TypeAdapterConfig<IngredientEntity, IngredientListModel>.NewConfig();
            TypeAdapterConfig<IngredientEntity, IngredientDetailModel>.NewConfig();
            TypeAdapterConfig<IngredientDetailModel, IngredientEntity>.NewConfig();

            // Configure mappings for Recipes
            TypeAdapterConfig<RecipeEntity, RecipeListModel>.NewConfig();
            TypeAdapterConfig<RecipeEntity, RecipeDetailModel>.NewConfig()
                .Map(dest => dest.IngredientAmounts, src => src.IngredientAmounts);
            TypeAdapterConfig<IngredientAmountEntity, RecipeDetailIngredientModel>.NewConfig();
            TypeAdapterConfig<RecipeDetailModel, RecipeEntity>.NewConfig()
                .Ignore(dest => dest.IngredientAmounts);
        }
    }
}