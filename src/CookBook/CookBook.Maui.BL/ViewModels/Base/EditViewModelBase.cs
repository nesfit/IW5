using System.Windows.Input;
using CookBook.Maui.BL.Facades;
using CookBook.Maui.BL.Factories;
using CookBook.Maui.BL.Services;

namespace CookBook.Maui.BL.ViewModels;

public abstract class EditViewModelBase<TDetailModel> : ViewModelWithParameterBase<Guid?>
{
    private readonly IDetailFacade<TDetailModel> detailFacade;
    private readonly INavigationService navigationService;

    public TDetailModel Item { get; set; }
    public ICommand SaveCommand { get; set; }

    public EditViewModelBase(Dependencies dependencies)
    {
        detailFacade = dependencies.DetailFacade;
        navigationService = dependencies.NavigationService;

        SaveCommand = dependencies.CommandFactory.CreateCommand(SaveAsync);
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        Item = (Parameter is null)
            ? GetNewItem()
            : await detailFacade.GetByIdAsync(Parameter.Value);
    }

    protected abstract TDetailModel GetNewItem();

    private async Task SaveAsync()
    {
        await detailFacade.UpsertAsync(Item);
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
