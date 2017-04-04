using CookBook.App.ViewModels;
using CookBook.BL;
using CookBook.BL.Repositories;

namespace CookBook.App
{
    public class ViewModelLocator
    {
        private readonly Messenger _messenger = new Messenger();
        private readonly RecipeRepository _recipeRepository = new RecipeRepository();

        public MainViewModel MainViewModel => CreateMainViewModel();

        public RecipeListViewModel RecipeListViewModel => CreateRecipeListViewModel();

        public RecipeDetailViewModel RecipeDetailViewModel => CreateRecipeDetailViewModel();


        private MainViewModel CreateMainViewModel()
        {
            return new MainViewModel(_messenger);
        }

        private RecipeDetailViewModel CreateRecipeDetailViewModel()
        {
            return new RecipeDetailViewModel(_recipeRepository, _messenger);
        }

        private RecipeListViewModel CreateRecipeListViewModel()
        {

            return new RecipeListViewModel(_recipeRepository, _messenger);
        }
    }
}