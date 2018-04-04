using System;
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

        // ToDo Add Recipes

        public ICommand SelectRecipeCommand { get; }

        public RecipeListViewModel(RecipeRepository recipeRepository, IMessenger messenger)
        {
            this.recipeRepository = recipeRepository;
            this.messenger = messenger;

            SelectRecipeCommand = new RelayCommand(RecipeSelectionChanged);
        }

        public void OnLoad()
        {
            // ToDo Load Data
        }

        public void RecipeSelectionChanged(object parameter)
        {
            var recipe = parameter as RecipeListModel;

            if (recipe == null)
            {
                return;
            }

            messenger.Send(new SelectedRecipeMessage() { Id = recipe.Id });
        }
    }
}