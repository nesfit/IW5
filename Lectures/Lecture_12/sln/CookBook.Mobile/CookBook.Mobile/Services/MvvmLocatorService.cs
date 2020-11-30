using CookBook.BL.Mobile.Ioc;
using CookBook.BL.Mobile.Services;
using CookBook.BL.Mobile.ViewModels;
using CookBook.Mobile.Views;
using System;
using Xamarin.Forms;

namespace CookBook.Mobile.Services
{
    public class MvvmLocatorService : IMvvmLocatorService
    {
        private readonly IDependencyInjectionService dependencyInjectionService;

        public MvvmLocatorService(IDependencyInjectionService dependencyInjectionService)
        {
            this.dependencyInjectionService = dependencyInjectionService;
        }

        public Page ResolveView<TViewModel>(TViewModel viewModel = default) where TViewModel : class, IViewModel
        {
            var viewType = GetViewType(viewModel);
            return GetView<TViewModel>(viewType);
        }

        private Type GetViewType<TViewModel>(TViewModel viewModel)
        {
            var viewModelType = viewModel?.GetType() ?? typeof(TViewModel);
            var viewTypeName = viewModelType
                .AssemblyQualifiedName
                .Replace(viewModelType.Assembly.GetName().Name, typeof(ViewBase).Assembly.GetName().Name)
                .Replace("ViewModel", "View");

            var viewType = Type.GetType(viewTypeName);
            if (viewType != null)
            {
                return viewType;
            }
            else
            {
                throw new Exception();
            }
        }

        private Page GetView<TViewModel>(Type viewType, TViewModel viewModel = null)
            where TViewModel : class, IViewModel
        {
            if (viewModel == null)
            {
                return dependencyInjectionService.Resolve(viewType) as Page;
            }
            else
            {
                return dependencyInjectionService.Resolve(viewType, new TypedParameter(typeof(TViewModel), viewModel)) as Page;
            }
        }
    }
}