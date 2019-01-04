using CookBook.App.Commands;
using CookBook.BL;
using CookBook.BL.Messages;
using CookBook.BL.Models;
using CookBook.BL.Repositories;
using CookBook.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CookBook.App.ViewModels
{
    public class RecipeDetailViewModel : ViewModelBase
    {
        private readonly RecipeRepository recipeRepository;
        private readonly IMessenger messenger;
        private RecipeDetailModel detail;

        public RecipeDetailModel Detail
        {
            get => detail;
            set
            {
                if (Equals(value, Detail)) return;

                detail = value;
                OnPropertyChanged();
            }
        }

        public IList<FoodType> FoodTypes => Enum.GetValues(typeof(FoodType)).Cast<FoodType>().ToList();
        public ICommand SaveRecipeCommand { get; set; }
        public ICommand DeleteRecipeCommand { get; set; }

        public RecipeDetailViewModel(RecipeRepository recipeRepository, IMessenger messenger)
        {
            this.recipeRepository = recipeRepository;
            this.messenger = messenger;

            SaveRecipeCommand = new SaveRecipeCommand(recipeRepository, this, messenger);
            DeleteRecipeCommand = new RelayCommand(DeleteRecipe);

            this.messenger.Register<SelectedRecipeMessage>(SelectedRecipe);
            this.messenger.Register<NewRecipeMessage>(NewRecipeMessageReceived);
        }

        private void DeleteRecipe()
        {
            if (Detail.Id != Guid.Empty)
            {
                var recipeId = Detail.Id;

                Detail = new RecipeDetailModel();
                recipeRepository.Remove(recipeId);
                messenger.Send(new DeletedRecipeMessage(recipeId));
            }
        }

        private void NewRecipeMessageReceived(NewRecipeMessage obj)
        {
            Detail = new RecipeDetailModel();
        }

        private void SelectedRecipe(SelectedRecipeMessage message)
        {
            Detail = recipeRepository.GetById(message.Id);
        }
    }
}