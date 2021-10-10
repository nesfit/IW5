using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;
using CookBook.Api.DAL.Memory;
using CookBook.Api.DAL.Memory.Repositories;
using CookBook.Common.Enums;
using Newtonsoft.Json;

namespace CookBook.API.DAL.IntegrationTests;

public class InMemoryDatabaseFixture : IDatabaseFixture
{
    public IngredientAmountEntity? GetIngredientAmountDirectly(Guid ingredientAmountId)
    {
        var ingredientAmount = inMemoryStorage.Value.IngredientAmounts.SingleOrDefault(t => t.Id == ingredientAmountId);

        return DeepClone(ingredientAmount);
    }

    public RecipeEntity? GetRecipeDirectly(Guid recipeId)
    {
        var recipe = inMemoryStorage.Value.Recipes.SingleOrDefault(t => t.Id == recipeId);
        if (recipe == null)
        {
            return null;
        }

        recipe.IngredientAmounts = inMemoryStorage.Value.IngredientAmounts.Where(t => t.RecipeId == recipeId).ToList();


        return DeepClone(recipe);
    }

    private T DeepClone<T>(T input)
    {
        var json = JsonConvert.SerializeObject(input);
        return JsonConvert.DeserializeObject<T>(json);
    }

    public IRecipeRepository GetRecipeRepository()
    {
        return new RecipeRepository(inMemoryStorage.Value);
    }

    public IList<Guid> IngredientGuids { get; } = new List<Guid>
    {
        new("df935095-8709-4040-a2bb-b6f97cb416dc"), new("23b3902d-7d4f-4213-9cf0-112348f56238")
    };

    public IList<Guid> IngredientAmountGuids { get; } = new List<Guid>
    {
        new("0d4fa150-ad80-4d46-a511-4c666166ec5e"), new("87833e66-05ba-4d6b-900b-fe5ace88dbd8")
    };

    public IList<Guid> RecipeGuids { get; } = new List<Guid> { new("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e") };

    private readonly Lazy<Storage> inMemoryStorage;

    public InMemoryDatabaseFixture()
    {
        inMemoryStorage = new Lazy<Storage>(CreateInMemoryStorage);
    }

    private Storage CreateInMemoryStorage()
    {
        var storage = new Storage(false);
        SeedStorage(storage);
        return storage;
    }

    private void SeedStorage(Storage storage)
    {
        storage.Ingredients.Add(new IngredientEntity(IngredientGuids[0], "Vejce", "Popis vajec",
            "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Chicken_egg_2009-06-04.jpg/428px-Chicken_egg_2009-06-04.jpg"));
        storage.Ingredients.Add(new IngredientEntity(IngredientGuids[1], "Cibule", "Popis cibule",
            "https://upload.wikimedia.org/wikipedia/commons/thumb/2/25/Onion_on_White.JPG/480px-Onion_on_White.JPG"));


        storage.IngredientAmounts.Add(
            new IngredientAmountEntity(IngredientAmountGuids[0], 4.0, Unit.Pieces, RecipeGuids[0], IngredientGuids[0])
            {
                Id = IngredientAmountGuids[0]
            });

        storage.IngredientAmounts.Add(
            new IngredientAmountEntity(IngredientAmountGuids[1], 1.0, Unit.Pieces, RecipeGuids[0], IngredientGuids[1])
            {
                Id = IngredientAmountGuids[1]
            });

        storage.Recipes.Add(new RecipeEntity(
            "Míchaná vejce",
            "Popis míchaných vajec",
            TimeSpan.FromMinutes(15),
            FoodType.MainDish,
            "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ef/Scrambled_eggs-01.jpg/320px-Scrambled_eggs-01.jpg")
        {
            Id = RecipeGuids[0]
        });
    }
}
