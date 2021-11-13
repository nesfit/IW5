using System.Windows.Input;
using CookBook.Maui.BL.Facades;
using CookBook.Maui.BL.Factories;
using CookBook.Maui.BL.Services;

namespace CookBook.Maui.BL.ViewModels;

public abstract class ListViewModelBase<TListModel> : ViewModelBase
{
    protected readonly IListFacade<TListModel> listFacade;
    protected readonly INavigationService navigationService;

    public List<TListModel> Items { get; set; }
    public ICommand NavigateToDetailViewCommand { get; }

    protected ListViewModelBase(Dependencies dependencies)
    {
        listFacade = dependencies.ListFacade;
        navigationService = dependencies.NavigationService;

        NavigateToDetailViewCommand = dependencies.CommandFactory.CreateCommand<Guid>(NavigateToDetailViewAsync);
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        Items = await listFacade.GetAllAsync();
    }

    private async Task NavigateToDetailViewAsync(Guid id)
    {
        await navigationService.PushAsync<IngredientDetailViewModel, Guid?>(id);
    }

    public class Dependencies
    {
        public IListFacade<TListModel> ListFacade { get; }
        public INavigationService NavigationService { get; }
        public ICommandFactory CommandFactory { get; }

        public Dependencies(
            IListFacade<TListModel> listFacade,
            INavigationService navigationService,
            ICommandFactory commandFactory)
        {
            ListFacade = listFacade;
            NavigationService = navigationService;
            CommandFactory = commandFactory;
        }
    }
}
