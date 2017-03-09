using CookBook.BL;
using Xunit;

namespace CookBook.Tests
{
    public class RecipeRepositoryTests
    {
        private RecipeRepository recipeRepository = new RecipeRepository();

        [Fact]
        public void FindByName_ExistRecipe_NotNull()
        {
            var recipe = recipeRepository.FindByName("Čokoládová torta");

            Assert.NotNull(recipe);
        }
    }
}