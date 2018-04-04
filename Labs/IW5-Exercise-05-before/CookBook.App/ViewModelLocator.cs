using CookBook.App.ViewModels;
using CookBook.BL;
using CookBook.BL.Repositories;

namespace CookBook.App
{
    public class ViewModelLocator
    {
        private readonly Messenger messenger = new Messenger();
        private readonly RecipeRepository recipeRepository = new RecipeRepository();

        public RecipeListViewModel RecipeListViewModel => CreateRecipeListViewModel();
        public RecipeDetailViewModel RecipeDetailViewModel => CreateRecipeDetailViewModel();

        private RecipeDetailViewModel CreateRecipeDetailViewModel()
        {
            return new RecipeDetailViewModel(recipeRepository, messenger);
        }

        private RecipeListViewModel CreateRecipeListViewModel()
        {

            return new RecipeListViewModel(recipeRepository, messenger);
        }
    }
}