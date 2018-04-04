namespace CookBook.App
{
    using CookBook.BL;
    using CookBook.BL.Models;

    public class MainViewModel
    {
        private readonly RecipeRepository recipeRepository = new RecipeRepository();

        public RecipeDetailModel Recipe { get; set; }

        public void Load()
        {
            Recipe = this.recipeRepository.FindByName("Čokoládová torta");
        }
    }
}