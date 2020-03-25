using CookBook.BL.Web.Facades;
using CookBook.Web.MVC.ViewModels.Ingredient;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CookBook.Web.MVC.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IngredientFacade _ingredientFacade;

        public IngredientController(
            IngredientFacade ingredientFacade)
        {
            _ingredientFacade = ingredientFacade;
        }

        public async Task<IActionResult> List()
        {
            var ingredients = await _ingredientFacade.GetAllAsync();
            var ingredientListViewModel = new IngredientListViewModel
            {
                Ingredients = ingredients
            };
            return View(ingredientListViewModel);
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var ingredient = await _ingredientFacade.GetByIdAsync(id);
            var ingredientDetailViewModel = new IngredientDetailViewModel
            {
                IngredientDetail = ingredient
            };
            return View(ingredientDetailViewModel);
        }
    }
}