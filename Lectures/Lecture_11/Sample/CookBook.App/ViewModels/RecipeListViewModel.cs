using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CookBook.App.Commands;
using CookBook.BL;
using CookBook.BL.Messages;
using CookBook.BL.Models;
using CookBook.BL.Repositories;

namespace CookBook.App.ViewModels
{
    public class RecipeListViewModel : ViewModelBase
    {
        private readonly RecipeRepository recipeRepository;
        private readonly IMessenger messenger;
        private readonly Mapper mapper;

        public ObservableCollection<RecipeListModel> Recipes { get; set; } = new ObservableCollection<RecipeListModel>();

        public ICommand SelectRecipeCommand { get; }
        public ICommand OnLoadCommand { get; set; }

        public RecipeListViewModel(RecipeRepository recipeRepository, IMessenger messenger, Mapper mapper)
        {
            this.recipeRepository = recipeRepository;
            this.messenger = messenger;
            this.mapper = mapper;

            SelectRecipeCommand = new RelayCommand(RecipeSelectionChanged);
            OnLoadCommand = new RelayCommand(OnLoad);
            this.messenger.Register<UpdatedRecipeMessage>(UpdatedRecipeMessageReceived);
        }

        private void UpdatedRecipeMessageReceived(UpdatedRecipeMessage recipeMessage)
        {
            var existingRecipe = Recipes.SingleOrDefault(recipe => recipe.Id == recipeMessage.Model.Id);
            var newRecipe = mapper.MapDetailModelToListModel(recipeMessage.Model);
            if (existingRecipe == null)
            {
                Recipes.Add(newRecipe);
            }
            else
            {
                var recipeIndex = Recipes.IndexOf(existingRecipe);
                Recipes[recipeIndex] = newRecipe;
            }
        }

        public void OnLoad()
        {
            Recipes.Clear();

            var recipes = recipeRepository.GetAll();
            foreach (var recipe in recipes)
            {
                Recipes.Add(recipe);
            }
        }

        private void RecipeSelectionChanged(object parameter)
        {
            if (parameter is RecipeListModel recipe)
            {
                messenger.Send(new SelectedRecipeMessage { Id = recipe.Id });
            }
        }
    }
}