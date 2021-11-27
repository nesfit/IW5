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
    public class IngredientDetailViewModel : MasterPageViewModel
    {
        private readonly IIngredientFacade ingredientFacade;

        [FromRoute("id")]
        public Guid Id { get; set; }

        public IngredientEditorViewModel EditorViewModel { get; set; }

        public IngredientDetailViewModel(IIngredientFacade ingredientFacade)
        {
            this.ingredientFacade = ingredientFacade;

            EditorViewModel = new IngredientEditorViewModel(ingredientFacade);
            EditorViewModel.OnModified += () =>
            {
                Context.RedirectToRoute("Ingredients");
            };
        }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                EditorViewModel.Data = ingredientFacade.GetById(Id);
            }

            return base.PreRender();
        }


    }
}

