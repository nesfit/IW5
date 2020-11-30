using CookBook.BL.Mobile.Api;
using CookBook.BL.Mobile.Facades;
using CookBook.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Forms;

namespace CookBook.Mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly IngredientsFacade ingredientsFacade;
        public ObservableCollection<IngredientListModel> Ingredients { get; set; } = new ObservableCollection<IngredientListModel>();

        public MainPage()
        {
            InitializeComponent();
            ingredientsFacade = new IngredientsFacade(new IngredientClient(new HttpClient()));
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var ingredientsList = await ingredientsFacade.GetIngredientsAsync();
            Ingredients.Clear();
            foreach (var ingredient in ingredientsList)
            {
                Ingredients.Add(ingredient);
            }
        }
    }
}
