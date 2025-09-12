using Mapster;
using CookBook.Common.Models;

namespace CookBook.Web.BL.Mapping
{
    public static class WebMapsterConfig
    {
        public static void RegisterMappings()
        {
            // Configure mappings for Web BL (Detail to List conversions)
            TypeAdapterConfig<IngredientDetailModel, IngredientListModel>.NewConfig();
            TypeAdapterConfig<RecipeDetailModel, RecipeListModel>.NewConfig();
        }
    }
}