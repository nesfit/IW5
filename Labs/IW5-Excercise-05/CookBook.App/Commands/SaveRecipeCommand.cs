using System;
using System.Windows.Input;
using CookBook.App.ViewModels;
using CookBook.BL;
using CookBook.BL.Models;
using CookBook.BL.Repositories;

namespace CookBook.App.Commands
{
    public class SaveRecipeCommand : ICommand
    {
        private readonly RecipeRepository _recipeRepository;
        private readonly RecipeDetailViewModel _viewModel;
        private readonly IMessenger _messenger;

        public SaveRecipeCommand(RecipeRepository recipeRepository, RecipeDetailViewModel viewModel, IMessenger messenger)
        {
            _recipeRepository = recipeRepository;
            _viewModel = viewModel;
            _messenger = messenger;
        }

        public bool CanExecute(object parameter)
        {
            var detail = parameter as RecipeDetailModel;

            if (detail != null)
            {
                return detail.Duration.TotalMinutes > 0;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            // ToDo
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}