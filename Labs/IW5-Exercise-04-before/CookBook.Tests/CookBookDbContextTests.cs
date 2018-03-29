using System.Linq;
using CookBook.BL;
using CookBook.DAL;
using Xunit;

namespace CookBook.Tests
{
    public class CookBookDbContextTests
    {

        [Fact]
        public void Database_Exists()
        {
            using (var cookBookDbContext = new CookBookDbContext())
            {
                Assert.True(cookBookDbContext.Database.Exists());
            }
        }

        private readonly RecipeRepository recipeRepository = new RecipeRepository();

        [Fact]
        public void FindByName_ChocolateCake_NotNull()
        {
            var recipe = recipeRepository.FindByName("Čokoládová torta");
            Assert.NotNull(recipe);
        }

        [Fact]
        public void FindByName_ChocolateCake2_Null()
        {
            var recipe = recipeRepository.FindByName("Čokoládová torta 2");
            Assert.Null(recipe);
        }

        [Fact]
        public void FindByName_ChocolateCake_ContainsEgg()
        {
            var recipe = recipeRepository.FindByName("Čokoládová torta");
            var containsEgg = recipe.Ingredients.Any(ingredient => ingredient.Ingredient.Name == "Vajíčko");
            Assert.True(containsEgg);
        }

        [Fact]
        public void FindByName_Ingredient_NotNull()
        {
            var recipe = recipeRepository.FindByName("Čokoládová torta");
            var containsEgg = recipe.Ingredients.Any(ingredient => ingredient.Ingredient.Name == "Vajíčko");
            Assert.True(containsEgg);
        }
    }
}
