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
        private readonly RecipeRepository _recipeRepository;
        private readonly IMessenger _messenger;

        // ToDo Add Recipes


        public ICommand SelectRecipeCommand { get; }

        public RecipeListViewModel(RecipeRepository recipeRepository, IMessenger messenger)
        {
            _recipeRepository = recipeRepository;
            _messenger = messenger;

            SelectRecipeCommand = new RelayCommand(RecipeSelectionChanged);
            // ToDo Add Action on message
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

            _messenger.Send(new SelectedRecipeMessage() { Id = recipe.Id });
        }
    }
}