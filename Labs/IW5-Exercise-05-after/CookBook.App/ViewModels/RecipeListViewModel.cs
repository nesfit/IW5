using System;
using System.Collections.ObjectModel;
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

        public ObservableCollection<RecipeListModel> Recipes { get; set; } = new ObservableCollection<RecipeListModel>();

        public ICommand SelectRecipeCommand { get; }

        public RecipeListViewModel(RecipeRepository recipeRepository, IMessenger messenger)
        {
            this.recipeRepository = recipeRepository;
            this.messenger = messenger;

            SelectRecipeCommand = new RelayCommand(RecipeSelectionChanged);
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