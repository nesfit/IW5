using System.Net;
using System.Net.Http.Json;
using CookBook.Common.Enums;
using CookBook.Common.Models;
using Xunit;

namespace CookBook.Api.App.EndToEndTests;

// Step 0 of the course: IdentityOptions:IsEnabled = false, no users, seeded records have no OwnerId.
public class IdentityDisabledModificationTests : IAsyncDisposable
{
    private readonly CookBookApiApplicationFactory application = new();

    [Fact]
    public async Task AnonymousClientCanUpdateUpsertAndDeleteOwnerlessSeededIngredient()
    {
        var client = application.CreateClient();
        var seeded = await GetFirstIngredientAsync(client);

        var update = await client.PutAsJsonAsync("/api/ingredient", seeded with { Name = "Updated" });
        var upsert = await client.PostAsJsonAsync("/api/ingredient/upsert", seeded with { Name = "Upserted" });
        var delete = await client.DeleteAsync($"/api/ingredient/{seeded.Id}");

        Assert.Equal(HttpStatusCode.OK, update.StatusCode);
        Assert.Equal(HttpStatusCode.OK, upsert.StatusCode);
        Assert.Equal(HttpStatusCode.OK, delete.StatusCode);
    }

    [Fact]
    public async Task AnonymousClientCanUpdateUpsertAndDeleteOwnerlessSeededRecipe()
    {
        var client = application.CreateClient();
        var recipes = await client.GetFromJsonAsync<List<RecipeListModel>>("/api/recipe");
        var seeded = await client.GetFromJsonAsync<RecipeDetailModel>($"/api/recipe/{recipes!.First().Id}");

        var update = await client.PutAsJsonAsync("/api/recipe", seeded! with { Name = "Updated" });
        var upsert = await client.PostAsJsonAsync("/api/recipe/upsert", seeded with { Name = "Upserted" });
        var delete = await client.DeleteAsync($"/api/recipe/{seeded.Id}");

        Assert.Equal(HttpStatusCode.OK, update.StatusCode);
        Assert.Equal(HttpStatusCode.OK, upsert.StatusCode);
        Assert.Equal(HttpStatusCode.OK, delete.StatusCode);
    }

    [Fact]
    public async Task AnonymousClientCanCreateAndModifyNewRecipe()
    {
        var client = application.CreateClient();
        var recipe = new RecipeDetailModel
        {
            Id = Guid.NewGuid(),
            Name = "Recipe",
            Description = "Description",
            ImageUrl = "https://example.com/recipe.jpg",
            Duration = TimeSpan.FromMinutes(5),
            FoodType = FoodType.Dessert,
            IngredientAmounts = []
        };

        var create = await client.PostAsJsonAsync("/api/recipe", recipe);
        var update = await client.PutAsJsonAsync("/api/recipe", recipe with { Name = "Updated" });
        var delete = await client.DeleteAsync($"/api/recipe/{recipe.Id}");

        Assert.Equal(HttpStatusCode.OK, create.StatusCode);
        Assert.Equal(HttpStatusCode.OK, update.StatusCode);
        Assert.Equal(HttpStatusCode.OK, delete.StatusCode);
    }

    private static async Task<IngredientDetailModel> GetFirstIngredientAsync(HttpClient client)
    {
        var ingredients = await client.GetFromJsonAsync<List<IngredientListModel>>("/api/ingredient");
        return (await client.GetFromJsonAsync<IngredientDetailModel>($"/api/ingredient/{ingredients!.First().Id}"))!;
    }

    public ValueTask DisposeAsync() => application.DisposeAsync();
}
