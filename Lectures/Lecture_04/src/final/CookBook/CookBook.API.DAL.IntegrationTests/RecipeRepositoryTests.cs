using System;
using System.Collections.Generic;
using System.Linq;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Common.Enums;
using Xunit;

namespace CookBook.API.DAL.IntegrationTests;

public class RecipeRepositoryTests
{
    public RecipeRepositoryTests()
    {
        dbFixture = new InMemoryDatabaseFixture();
    }

    private readonly IDatabaseFixture dbFixture;

    [Fact]
    public void GetById_Returns_Requested_Recipe_Including_Their_IngredientAmounts()
    {
        //arrange
        var recipeRepository = dbFixture.GetRecipeRepository();

        //act
        var recipe = recipeRepository.GetById(dbFixture.RecipeGuids[0]);

        //assert
        Assert.Equal(dbFixture.RecipeGuids[0], recipe.Id);
        Assert.Equal("Míchaná vejce",recipe.Name);

        Assert.Equal(2, recipe.IngredientAmounts.Count);
        var ingredientAmount1 =
            Assert.Single(recipe.IngredientAmounts.Where(entity => entity.Id == dbFixture.IngredientAmountGuids[0]));
        var ingredientAmount2 =
            Assert.Single(recipe.IngredientAmounts.Where(entity => entity.Id == dbFixture.IngredientAmountGuids[1]));

        Assert.Equal(dbFixture.RecipeGuids[0], ingredientAmount1.RecipeId);
        Assert.Equal(dbFixture.RecipeGuids[0], ingredientAmount2.RecipeId);

        Assert.NotNull(ingredientAmount1.Ingredient);
        Assert.Equal("Vejce", ingredientAmount1.Ingredient.Name);

        Assert.NotNull(ingredientAmount2.Ingredient);
        Assert.Equal("Cibule", ingredientAmount2.Ingredient.Name);
    }

    [Fact]
    public void Insert_Saves_Recipe_And_IngredientAmounts()
    {
        //arrange
        var recipeRepository = dbFixture.GetRecipeRepository();

        var ingredientAmountId = Guid.NewGuid();

        var recipeId = Guid.NewGuid();
        var foodType = FoodType.MainDish;
        var duration = TimeSpan.FromMinutes(5);
        var newRecipe = new RecipeEntity{
            Id = recipeId,
            Name = "Name",
            Description = "Description",
            Duration = duration,
            FoodType = foodType,
            ImageUrl = "ImageUrl",
            IngredientAmounts = new List<IngredientAmountEntity>()
            {
                new IngredientAmountEntity
                {
                    Id = ingredientAmountId,
                    Amount = 5,
                    Unit = Unit.Pieces,
                    RecipeId = recipeId,
                    IngredientId = dbFixture.IngredientGuids[0]
                }
            }
        };
        
        //act
        recipeRepository.Insert(newRecipe);

        //assert
        var recipe = dbFixture.GetRecipeDirectly(recipeId);
        Assert.NotNull(recipe);
        Assert.Equal(foodType,recipe.FoodType);
        Assert.Equal(duration,recipe.Duration);

        var ingredientAmount = dbFixture.GetIngredientAmountDirectly(ingredientAmountId);
        Assert.NotNull(ingredientAmount);
    }

    [Fact]
    public void Update_Saves_NewIngredientAmount()
    {
        //arrange
        var recipeRepository = dbFixture.GetRecipeRepository();

        var recipeId = dbFixture.RecipeGuids[0];
        var recipe = dbFixture.GetRecipeDirectly(recipeId);
        var ingredientAmountId = Guid.NewGuid();

        var newIngredientAmount = 
            new IngredientAmountEntity
            {
                Id = ingredientAmountId,
                Amount = 5,
                Unit = Unit.Pieces,
                RecipeId = recipeId,
                IngredientId = dbFixture.IngredientGuids[0]
            };

        //act
        recipe.IngredientAmounts.Add(newIngredientAmount);
        recipeRepository.Update(recipe);

        //assert
        var recipeFromDb = dbFixture.GetRecipeDirectly(recipeId);
        Assert.NotNull(recipeFromDb);
        Assert.Single(recipeFromDb.IngredientAmounts.Where(t => t.Id == ingredientAmountId));

        var ingredientAmountFromDb = dbFixture.GetIngredientAmountDirectly(ingredientAmountId);
        Assert.NotNull(ingredientAmountFromDb);

    }

    [Fact]
    public void Update_Also_Updates_IngredientAmount()
    {
        //arrange
        var recipeRepository = dbFixture.GetRecipeRepository();

        var recipeId = dbFixture.RecipeGuids[0];
        var recipe = dbFixture.GetRecipeDirectly(recipeId);
        var ingredientAmount = recipe.IngredientAmounts.First();
        
        //act
        ingredientAmount.Amount = 3;
        recipeRepository.Update(recipe);

        //assert
        var recipeFromDb = dbFixture.GetRecipeDirectly(recipeId);
        Assert.NotNull(recipeFromDb);
        var ingredientAmountFromDb = recipeFromDb.IngredientAmounts.First();
        Assert.Equal(ingredientAmount.Id,ingredientAmountFromDb.Id);
        Assert.Equal(3,ingredientAmountFromDb.Amount);
    }

    [Fact]
    public void Update_Removes_IngredientAmount()
    {
        //arrange
        var recipeRepository = dbFixture.GetRecipeRepository();

        var recipeId = dbFixture.RecipeGuids[0];
        var recipe = dbFixture.GetRecipeDirectly(recipeId);
        var ingredientAmountId = recipe.IngredientAmounts.First().Id;

        //act
        recipe.IngredientAmounts.Clear();
        recipeRepository.Update(recipe);

        //assert
        var recipeFromDb = dbFixture.GetRecipeDirectly(recipeId);
        Assert.NotNull(recipeFromDb);
        Assert.Empty(recipeFromDb.IngredientAmounts);

        var ingredientAmountFromDb = dbFixture.GetIngredientAmountDirectly(ingredientAmountId);
        Assert.Null(ingredientAmountFromDb);
    }
}
