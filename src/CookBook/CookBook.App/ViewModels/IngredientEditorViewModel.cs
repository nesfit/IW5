using System;
using CookBook.Api.BL.Facades;
using CookBook.Common.Models;
using DotVVM.Framework.ViewModel;

namespace CookBook.App.ViewModels
{
    public class IngredientEditorViewModel : DotvvmViewModelBase
    {
        private readonly IIngredientFacade ingredientFacade;

        public IngredientDetailModel Data { get; set; }

        public Guid EmptyGuid => Guid.Empty;

        public event Action OnModified;

        public IngredientEditorViewModel(IIngredientFacade ingredientFacade)
        {
            this.ingredientFacade = ingredientFacade;
        }
        
        public void Save()
        {
            ingredientFacade.CreateOrUpdate(Data);
            OnModified?.Invoke();
        }

        public void Delete()
        {
            ingredientFacade.Delete(Data.Id);
            OnModified?.Invoke();
        }
    }
}
