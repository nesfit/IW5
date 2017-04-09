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
                return detail.Duration.TotalMinutes > 0
                    && !string.IsNullOrWhiteSpace(detail.Name);
            }

            return false;
        }

        public void Execute(object parameter)
        {
            var detail = parameter as RecipeDetailModel;

            if (detail == null)
            {
                return;
            }

            if (detail.Id != Guid.Empty)
            {
                _recipeRepository.Update(detail);
            }
            else
            {
                _viewModel.Detail = _recipeRepository.Insert(detail);
            }

            _messenger.Send(new UpdatedRecipeMessage(detail));
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}