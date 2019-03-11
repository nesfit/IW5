using System.Threading;
using System.Threading.Tasks;
using IW5_Swagger.ConsoleApp.API.Models;

namespace IW5_Swagger.ConsoleApp.API
{
    public partial class APIClientExtensions
    {
        public static void RecipesInsertItem(this IAPIClient operations, RecipeModelInner recipe)
        {
            operations.RecipesInsertItem(recipe.Name, recipe.Description, recipe.FoodType.ToString(), recipe.Id);
        }

        public static async Task RecipesInsertItemAsync(this IAPIClient operations, RecipeModelInner recipe, CancellationToken cancellationToken = default)
        {
            await operations.RecipesInsertItemAsync(recipe.Name, recipe.Description, recipe.FoodType.ToString(), recipe.Id, cancellationToken);
        }
    }
}