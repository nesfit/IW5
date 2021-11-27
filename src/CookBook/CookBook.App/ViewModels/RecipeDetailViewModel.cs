using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.Api.BL.Facades;
using CookBook.App.Models;
using CookBook.Common.Enums;
using CookBook.Common.Models;
using DotVVM.Framework.ViewModel;

namespace CookBook.App.ViewModels
{
    public class RecipeDetailViewModel : MasterPageViewModel
    {
        private readonly IRecipeFacade recipeFacade;
        private readonly IIngredientFacade ingredientFacade;

        [FromRoute("id")]
        public Guid Id { get; set; }

        public RecipeDetailModel Data { get; set; }

        public RecipeDetailIngredientModel NewIngredientModel { get; set; } = new();

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<IngredientListModel> Ingredients { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public IEnumerable<EnumItem<FoodType>> FoodTypes { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public IEnumerable<EnumItem<Unit>> Units { get; set; }

        public RecipeDetailViewModel(IRecipeFacade recipeFacade, IIngredientFacade ingredientFacade)
        {
            this.recipeFacade = recipeFacade;
            this.ingredientFacade = ingredientFacade;
        }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                Ingredients = ingredientFacade.GetAll();
                FoodTypes = Enum.GetValues<FoodType>().Select(v => new EnumItem<FoodType>(v));
                Units = Enum.GetValues<Unit>().Select(v => new EnumItem<Unit>(v));

                if (Id != Guid.Empty)
                {
                    Data = recipeFacade.GetById(Id);
                }
                else
                {
                    Data = new RecipeDetailModel();
                }
            }
            return base.PreRender();
        }

        public void Save()
        {
            recipeFacade.CreateOrUpdate(Data);
            Context.RedirectToRoute("Recipes");
        }

        public void Delete()
        {
            recipeFacade.Delete(Data.Id);
            Context.RedirectToRoute("Recipes");
        }

        public void AddIngredientAmount()
        {
            NewIngredientModel.Ingredient = ingredientFacade.GetAll().Single(i => i.Id == NewIngredientModel.IngredientId);
            Data.IngredientAmounts.Add(NewIngredientModel);
            NewIngredientModel = new();
        }

        public void RemoveIngredientAmount(RecipeDetailIngredientModel item)
        {
            Data.IngredientAmounts.Remove(item);
        }
    }
}

