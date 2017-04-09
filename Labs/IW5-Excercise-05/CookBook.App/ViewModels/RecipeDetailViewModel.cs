using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CookBook.App.Commands;
using CookBook.BL;
using CookBook.BL.Messages;
using CookBook.BL.Models;
using CookBook.BL.Repositories;
using CookBook.DAL.Entities;

namespace CookBook.App.ViewModels
{
    public class RecipeDetailViewModel : ViewModelBase
    {
        private readonly RecipeRepository _recipeRepository;
        private readonly IMessenger _messenger;
        private RecipeDetailModel _detail;

        public RecipeDetailModel Detail
        {
            get { return _detail; }
            set
            {
                if (Equals(value, Detail)) return;

                _detail = value;
                OnPropertyChanged();
            }
        }

        public IList<FoodType> FoodTypes => Enum.GetValues(typeof(FoodType)).Cast<FoodType>().ToList();

        public ICommand DeleteCommand { get; }

        public ICommand SaveCommand { get; }

        public RecipeDetailViewModel(RecipeRepository recipeRepository, IMessenger messenger)
        {
            _recipeRepository = recipeRepository;
            _messenger = messenger;
            DeleteCommand = new RelayCommand(Delete);
            SaveCommand = new SaveRecipeCommand(recipeRepository, this, messenger);

            _messenger.Register<SelectedRecipeMessage>(SelectedRecipe);
            _messenger.Register<NewRecipeMessage>(NewRecipeMessageReceived);
        }

        private void NewRecipeMessageReceived(NewRecipeMessage message)
        {
            Detail = new RecipeDetailModel();
        }

        private void SelectedRecipe(SelectedRecipeMessage message)
        {
            Detail = _recipeRepository.GetById(message.Id);
        }

        public void Delete()
        {
            if (IsSavedRecipe())
            {
                _recipeRepository.Remove(Detail.Id);
                _messenger.Send(new DeletedRecipeMessage(Detail.Id));
            }

            Detail = null;
        }

        private bool IsSavedRecipe()
        {
            return Detail.Id != Guid.Empty;
        }
    }
}