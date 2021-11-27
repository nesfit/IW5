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
    public class RecipesViewModel : MasterPageViewModel
    {
        private readonly IRecipeFacade recipeFacade;

        public List<RecipeListModel> Recipes { get; set; }

        public RecipesViewModel(IRecipeFacade recipeFacade)
        {
            this.recipeFacade = recipeFacade;
        }

        public override Task PreRender()
        {
            Recipes = recipeFacade.GetAll();
            return base.PreRender();
        }
    }
}

