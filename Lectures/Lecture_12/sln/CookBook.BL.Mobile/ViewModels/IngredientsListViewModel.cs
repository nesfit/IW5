using CookBook.BL.Mobile.Facades;
using CookBook.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CookBook.BL.Mobile.ViewModels
{
    public class IngredientsListViewModel : ViewModelBase
    {
        private readonly IngredientsFacade ingredientsFacade;

        public ObservableCollection<IngredientListModel> Ingredients { get; set; } = new ObservableCollection<IngredientListModel>();

        public IngredientsListViewModel(IngredientsFacade ingredientsFacade)
        {
            this.ingredientsFacade = ingredientsFacade;
        }

        public override async Task OnAppearing()
        {
            await base.OnAppearing();

            //ingredientsFacade.GetIngredientsAsync().
            try
            {
                var ingredients = await ingredientsFacade.GetIngredientsAsync();
                Ingredients.Clear();
                foreach (var ingredient in ingredients)
                {
                    Ingredients.Add(ingredient);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}