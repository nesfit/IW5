using System.Windows.Input;
using CookBook.Maui.BL.Facades;
using CookBook.Maui.BL.Factories;
using CookBook.Maui.BL.Services;

namespace CookBook.Maui.BL.ViewModels;

public abstract class DetailViewModelBase<TDetailModel> : ViewModelWithParameterBase<Guid?>
{
    protected readonly IDetailFacade<TDetailModel> detailFacade;
    protected readonly INavigationService navigationService;

    public TDetailModel Item { get; set; }
    public ICommand NavigateToEditViewCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    protected DetailViewModelBase(Dependencies dependencies)
    {
        detailFacade = dependencies.DetailFacade;
        navigationService = dependencies.NavigationService;

        NavigateToEditViewCommand = dependencies.CommandFactory.CreateCommand(NavigateToEditViewAsync);
        DeleteCommand = dependencies.CommandFactory.CreateCommand(DeleteAsync);
    }

    public override async Task OnAppearingAsync()
    {
        if (Parameter is not null)
        {
            Item = await detailFacade.GetByIdAsync(Parameter.Value);
        }
    }

    private async Task NavigateToEditViewAsync()
    {
        if (Parameter is not null)
        {
            await navigationService.PushAsync<IngredientEditViewModel, Guid?>(Parameter.Value);
        }
    }

    private async Task DeleteAsync()
    {
        if (Parameter is not null)
        {
            await detailFacade.DeleteAsync(Parameter.Value);
        }

        await navigationService.PopAsync();
    }

    public class Dependencies
    {
        public IDetailFacade<TDetailModel> DetailFacade { get; }
        public INavigationService NavigationService { get; }
        public ICommandFactory CommandFactory { get; }

        public Dependencies(
            IDetailFacade<TDetailModel> detailFacade,
            INavigationService navigationService,
            ICommandFactory commandFactory)
        {
            DetailFacade = detailFacade;
            NavigationService = navigationService;
            CommandFactory = commandFactory;
        }
    }
}
