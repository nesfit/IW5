using System;
using System.Linq;
using CookBook.BL;
using CookBook.BL.Repositories;
using CookBook.DAL;
using Xunit;

namespace CookBook.Tests
{
    public class CookBookDbContextTests
    {

        [Fact]
        public void DbConnectionTest()
        {
            using (var cookBookDbContext = new CookBookDbContext())
            {
                cookBookDbContext.Recipes.Any();
            }
        }

        private RecipeRepository recipeRepositorySUT = new RecipeRepository();

        [Fact]
        public void FindByName_ChocolateCake_NotNull()
        {
            var recipe = recipeRepositorySUT.FindByName("Čokoládová torta");
            Assert.NotNull(recipe);
        }

        [Fact]
        public void FindByName_ChocolateCake2_Null()
        {
            var recipe = recipeRepositorySUT.FindByName("Čokoládová torta 2");
            Assert.Null(recipe);
        }

        [Fact]
        public void FindByName_ChocolateCake_ContainsEgg()
        {
            var recipe = recipeRepositorySUT.FindByName("Čokoládová torta");
            var containsEgg = recipe.Ingredients.Any(ingredient => ingredient.Ingredient.Name == "Vajíčko");
            Assert.True(containsEgg);
        }

        [Fact]
        public void FindByName_Ingredient_NotNull()
        {
            var recipe = recipeRepositorySUT.FindByName("Čokoládová torta");
            var containsEgg = recipe.Ingredients.Any(ingredient => ingredient.Ingredient.Name == "Vajíčko");
            Assert.True(containsEgg);
        }
    }
}
