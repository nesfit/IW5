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
        private readonly RecipeRepository recipeRepository;
        private readonly RecipeDetailViewModel viewModel;
        private readonly IMessenger messenger;

        public SaveRecipeCommand(RecipeRepository recipeRepository, RecipeDetailViewModel viewModel, IMessenger messenger)
        {
            this.recipeRepository = recipeRepository;
            this.viewModel = viewModel;
            this.messenger = messenger;
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

        public event EventHandler CanExecuteChanged;
    }
}