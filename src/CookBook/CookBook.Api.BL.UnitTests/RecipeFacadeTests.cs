using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CookBook.Api.BL.Facades;
using CookBook.Api.DAL.Common.Repositories;
using CookBook.Common.Enums;
using CookBook.Common.Models;
using Moq;
using Xunit;

namespace CookBook.Api.BL.UnitTests;
public class RecipeFacadeTests
{
    [Fact]
    public void Delete_Calls_Correct_Method_On_Repository()
    {
        //arrange
        var repositoryMock = new Mock<IRecipeRepository>(MockBehavior.Strict);
        repositoryMock.Setup(recipeRepository => recipeRepository.Remove(It.IsAny<Guid>()));

        var repository = repositoryMock.Object;
        var mapper = new Mock<IMapper>(MockBehavior.Strict).Object;
        var facade = new RecipeFacade(repository, mapper);

        var itemId = Guid.NewGuid();
        //act
        facade.Delete(itemId);

        //assert
        repositoryMock.Verify(recipeRepository => recipeRepository.Remove(itemId));
    }

    [Fact]
    public void MergeIngredientAmounts_Merges_Recipe_With_Multiple_IngredientAmounts_Of_Same_Ingredient_And_Unit()
    {
        //arrange
        var facade = GetFacadeWithForbiddenDependencyCalls();

        var mergedIngredientId = Guid.NewGuid();
        var recipe = new RecipeDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Description = "Description",
            ImageUrl = "Url",
            Duration = TimeSpan.FromMinutes(5),
            FoodType = FoodType.Dessert,
            IngredientAmounts = new List<RecipeDetailIngredientModel>()
            {
                new RecipeDetailIngredientModel
                {
                    Id = Guid.NewGuid(),
                    Amount = 5,
                    Unit = Unit.Pieces,
                    Ingredient = new IngredientListModel
                    {
                        Id = mergedIngredientId,
                        Name = "MergedIngredient",
                    }
                },
                new RecipeDetailIngredientModel
                {
                    Id = Guid.NewGuid(),
                    Amount = 2,
                    Unit = Unit.Pieces,
                    Ingredient = new IngredientListModel
                    {
                        Id = mergedIngredientId,
                        Name = "MergedIngredient",
                    }
                }
            }
        };
        //act
        facade.MergeIngredientAmounts(recipe);


        //assert
        var mergedIngredient = Assert.Single(recipe.IngredientAmounts);
        Assert.Equal(7, mergedIngredient.Amount);
        Assert.Equal(mergedIngredientId, mergedIngredient.Ingredient.Id);
    }

    [Fact]
    public void MergeIngredientAmounts_Does_Not_Merge_Recipe_With_Multiple_IngredientAmounts_Of_Same_Ingredient_And_Different_Units()
    {
        //arrange
        var facade = GetFacadeWithForbiddenDependencyCalls();

        var mergedIngredientId = Guid.NewGuid();
        var ingredientAmount1Id = Guid.NewGuid();
        var ingredientAmount2Id = Guid.NewGuid();
        var recipe = new RecipeDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Description = "Description",
            ImageUrl = "Url",
            Duration = TimeSpan.FromMinutes(5),
            FoodType = FoodType.Dessert,
            IngredientAmounts = new List<RecipeDetailIngredientModel>()
            {
                new RecipeDetailIngredientModel
                {
                    Id = ingredientAmount1Id,
                    Amount = 5,
                    Unit = Unit.Pieces,
                    Ingredient = new IngredientListModel
                    {
                        Id = mergedIngredientId,
                        Name = "MergedIngredient",
                    }
                },
                new RecipeDetailIngredientModel{
                    Id = ingredientAmount2Id,
                    Amount = 2,
                    Unit = Unit.Kg,
                    Ingredient = new IngredientListModel
                    {
                        Id = mergedIngredientId,
                        Name = "MergedIngredient",
                    }}
            }
        };

        //act
        facade.MergeIngredientAmounts(recipe);

        //assert
        Assert.Equal(2, recipe.IngredientAmounts.Count);
        var ingredientAmount1 = Assert.Single(recipe.IngredientAmounts.Where(t => t.Id == ingredientAmount1Id));
        var ingredientAmount2 = Assert.Single(recipe.IngredientAmounts.Where(t => t.Id == ingredientAmount2Id));

        Assert.Equal(5, ingredientAmount1.Amount);
        Assert.Equal(Unit.Pieces, ingredientAmount1.Unit);

        Assert.Equal(2, ingredientAmount2.Amount);
        Assert.Equal(Unit.Kg, ingredientAmount2.Unit);
    }

    [Fact]
    public void MergeIngredientAmounts_Does_Not_Merge_Recipe_With_Multiple_IngredientAmounts_Of_Different_Ingredient_And_Same_Units()
    {
        //arrange
        var facade = GetFacadeWithForbiddenDependencyCalls();


        var notIngredientId = Guid.NewGuid();
        var not1IngredientId = Guid.NewGuid();
        var ingredientAmount1Id = Guid.NewGuid();
        var ingredientAmount2Id = Guid.NewGuid();
        var recipe = new RecipeDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Description = "Description",
            ImageUrl = "Url",
            Duration = TimeSpan.FromMinutes(5),
            FoodType = FoodType.Dessert,
            IngredientAmounts = new List<RecipeDetailIngredientModel>()
            {
                new RecipeDetailIngredientModel
                {
                    Id = ingredientAmount1Id,
                    Amount = 5,
                    Unit = Unit.Pieces,
                    Ingredient = new IngredientListModel
                    {
                        Id = notIngredientId,
                        Name = "NotMergedIngredient",
                    }},
                new RecipeDetailIngredientModel
                {
                    Id = ingredientAmount2Id,
                    Amount = 2,
                    Unit = Unit.Pieces,
                    Ingredient = new IngredientListModel
                    {
                        Id = not1IngredientId,
                        Name = "NotMergedIngredient2",
                    }}
            }
        };

        //act
        facade.MergeIngredientAmounts(recipe);

        //assert
        Assert.Equal(2, recipe.IngredientAmounts.Count);
        var ingredientAmount1 = Assert.Single(recipe.IngredientAmounts.Where(t => t.Id == ingredientAmount1Id));
        var ingredientAmount2 = Assert.Single(recipe.IngredientAmounts.Where(t => t.Id == ingredientAmount2Id));

        Assert.Equal(5, ingredientAmount1.Amount);
        Assert.Equal(Unit.Pieces, ingredientAmount1.Unit);

        Assert.Equal(2, ingredientAmount2.Amount);
        Assert.Equal(Unit.Pieces, ingredientAmount2.Unit);
    }

    [Fact]
    public void MergeIngredientAmounts_Does_Not_Fail_When_Recipe_Has_No_Ingredients()
    {
        //arrange
        var facade = GetFacadeWithForbiddenDependencyCalls();
        var recipe = new RecipeDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Description = "Description",
            ImageUrl = "Url",
            Duration = TimeSpan.FromMinutes(5),
            FoodType = FoodType.Dessert,
            IngredientAmounts = new List<RecipeDetailIngredientModel>()
            {
            }
        };
        //act
        facade.MergeIngredientAmounts(recipe);

        //assert
        Assert.Empty(recipe.IngredientAmounts);
    }


    private static RecipeFacade GetFacadeWithForbiddenDependencyCalls()
    {
        var repository = new Mock<IRecipeRepository>(MockBehavior.Strict).Object;
        var mapper = new Mock<IMapper>(MockBehavior.Strict).Object;
        var facade = new RecipeFacade(repository, mapper);
        return facade;
    }
}
