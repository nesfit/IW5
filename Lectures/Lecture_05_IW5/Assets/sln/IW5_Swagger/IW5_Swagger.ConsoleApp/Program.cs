using System;
using System.Threading.Tasks;
using IW5_Swagger.ConsoleApp.API;
using IW5_Swagger.ConsoleApp.API.Models;

namespace IW5_Swagger.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var apiClient = new APIClient("https://iw5swaggerdemo.azurewebsites.net");

            var recipeModel = await apiClient.RecipesGetItemAsync(new Guid("56A1B6CF-80B8-4963-9CBE-35BE4E78929A"));

            await apiClient.RecipesInsertItemAsync(
                new RecipeModelInner
                {
                    Id = Guid.NewGuid(),
                    Name = "Guláš",
                    Description = "Popis guláše",
                    FoodType = FoodType.MainDish
                });

            var recipesAll = await apiClient.RecipesGetAllItemsAsync();
        }
    }
}
