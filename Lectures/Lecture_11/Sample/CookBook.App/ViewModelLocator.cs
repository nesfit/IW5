using CookBook.App.ViewModels;
using CookBook.BL;
using CookBook.BL.Repositories;

namespace CookBook.App
{
    public class ViewModelLocator
    {
        private readonly Messenger messenger = new Messenger();
        private readonly RecipeRepository recipeRepository = new RecipeRepository();
        private readonly Mapper mapper = new Mapper();

        public RecipeListViewModel RecipeListViewModel => CreateRecipeListViewModel();
        public RecipeDetailViewModel RecipeDetailViewModel => CreateRecipeDetailViewModel();
        public MainViewModel MainViewModel => CreateMainViewModel();

        private MainViewModel CreateMainViewModel()
        {
            return new MainViewModel(messenger);
        }

        private RecipeDetailViewModel CreateRecipeDetailViewModel()
        {
            return new RecipeDetailViewModel(recipeRepository, messenger);
        }

        private RecipeListViewModel CreateRecipeListViewModel()
        {
            return new RecipeListViewModel(recipeRepository, messenger, mapper);
        }
    }
}