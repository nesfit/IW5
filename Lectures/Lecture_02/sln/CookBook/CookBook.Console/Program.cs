using CookBook.Console.Api;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookBook.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var httpClient = new HttpClient();
            var ingredientClient = new IngredientClient(httpClient);
            var ingredients = await ingredientClient.IngredientGetAllAsync();
        }
    }
}
