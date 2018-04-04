using System;
using System.Collections.Generic;
using System.Linq;
using CookBook.BL;
using CookBook.BL.Messages;
using CookBook.BL.Models;
using CookBook.BL.Repositories;
using CookBook.DAL.Entities;

namespace CookBook.App.ViewModels
{
    public class RecipeDetailViewModel : ViewModelBase
    {
        private readonly RecipeRepository recipeRepository;
        private readonly IMessenger messenger;
        private RecipeDetailModel detail;

        public RecipeDetailModel Detail
        {
            get { return detail; }
            set
            {
                if (Equals(value, Detail)) return;

                detail = value;
                OnPropertyChanged();
            }
        }

        public IList<FoodType> FoodTypes => Enum.GetValues(typeof(FoodType)).Cast<FoodType>().ToList();

        public RecipeDetailViewModel(RecipeRepository recipeRepository, IMessenger messenger)
        {
            this.recipeRepository = recipeRepository;
            this.messenger = messenger;

            this.messenger.Register<SelectedRecipeMessage>(SelectedRecipe);
        }

        private void SelectedRecipe(SelectedRecipeMessage message)
        {
            Detail = recipeRepository.GetById(message.Id);
        }
    }
}