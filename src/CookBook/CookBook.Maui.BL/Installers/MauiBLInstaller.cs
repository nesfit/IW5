using CookBook.Common.Installers;
using CookBook.Maui.BL.Facades;
using CookBook.Maui.BL.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Maui.BL.Installers;

public class MauiBLInstaller : IInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IIngredientFacade, IngredientFacade>();
        
        serviceCollection.AddSingleton<IngredientListViewModel>();
    }
}
