using System;
using System.Windows.Input;
using CookBook.App.ViewModels;
using CookBook.BL;
using CookBook.BL.Messages;
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
            return viewModel.Detail?.Duration.TotalMinutes > 0;
        }

        public void Execute(object parameter)
        {
            if (viewModel.Detail.Id == Guid.Empty)
            {
                recipeRepository.Insert(viewModel.Detail);
            }
            else
            {
                recipeRepository.Update(viewModel.Detail);
            }

            messenger.Send(new UpdatedRecipeMessage(viewModel.Detail));
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}