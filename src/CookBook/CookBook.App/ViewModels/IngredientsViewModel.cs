using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.Api.BL.Facades;
using CookBook.Common.Models;
using DotVVM.Framework.ViewModel;

namespace CookBook.App.ViewModels
{
    public class IngredientsViewModel : MasterPageViewModel
    {
        private readonly IIngredientFacade ingredientFacade;

        public List<IngredientListModel> Ingredients { get; set; }

        public IngredientEditorViewModel EditorViewModel { get; set; }

        public IngredientsViewModel(IIngredientFacade ingredientFacade)
        {
            this.ingredientFacade = ingredientFacade;

            EditorViewModel = new IngredientEditorViewModel(ingredientFacade);
            EditorViewModel.OnModified += LoadData;
        }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                LoadData();                
            }
            return base.PreRender();
        }

        private void LoadData()
        {
            Ingredients = ingredientFacade.GetAll();
            EditorViewModel.Data = new IngredientDetailModel();
        }

    }
}

