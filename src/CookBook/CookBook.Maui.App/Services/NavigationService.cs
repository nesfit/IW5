using System;
using System.Threading.Tasks;
using CookBook.Maui.App.Installers;
using CookBook.Maui.App.Views;
using CookBook.Maui.BL.Services;
using CookBook.Maui.BL.ViewModels;
using Microsoft.Maui.Controls;

namespace CookBook.Maui.App.Services;

public class NavigationService : INavigationService
{
    private readonly IDependencyInjectionService dependencyInjectionService;
    
    public NavigationPage RootNavigationPage { get; }

    public NavigationService(
        IDependencyInjectionService dependencyInjectionService,
        NavigationPage rootNavigationPage)
    {
        RootNavigationPage = rootNavigationPage;

        this.dependencyInjectionService = dependencyInjectionService;
    }

    public async Task PushAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var viewModel = dependencyInjectionService.GetRequiredService<TViewModel>();
        
        var viewType = GetViewType(viewModel.GetType());
        await PushAsync(viewType, viewModel);
    }

    public async Task PushAsync<TViewModel, TDetailModel, TParameter>(TParameter parameter)
        where TViewModel : IViewModelWithParameter<TDetailModel, TParameter>
    {
        var viewModel = dependencyInjectionService.GetRequiredService<TViewModel>();
        viewModel.Parameter = parameter;

        var viewType = GetViewType(viewModel.GetType());
        await PushAsync(viewType, viewModel);
    }

    public async Task PopAsync()
    {
        await RootNavigationPage.Navigation.PopAsync();
    }

    private async Task PushAsync<TViewModel>(Type viewType, TViewModel viewModel)
        where TViewModel : IViewModel
    {
        var contentPage = GetView(viewType, viewModel);
        await RootNavigationPage.Navigation.PushAsync(contentPage);
    }

    private ContentPage GetView<TViewModel>(Type viewType, TViewModel viewModel)
        where TViewModel : IViewModel
    {
        var contentPage = (ContentPageBase) dependencyInjectionService.GetRequiredService(viewType);
        contentPage.ViewModel = viewModel;

        return contentPage;
    }

    private Type? GetViewType(Type viewModelType)
    {
        var viewTypeName = viewModelType
            .AssemblyQualifiedName
            ?.Replace(viewModelType.Assembly.GetName().Name, typeof(MauiAppInstaller).Assembly.GetName().Name)
            .Replace("ViewModel", "View");

        return Type.GetType(viewTypeName);
    }
}
