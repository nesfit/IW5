using CookBook.Maui.App.Views;
using Microsoft.Maui.Controls;

namespace CookBook.Maui.App
{
	public partial class App : Application
	{
		public App(IngredientListView page)
		{
			InitializeComponent();

			MainPage = page;
		}
	}
}
