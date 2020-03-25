using CookBook.BL.Web.Api;
using CookBook.BL.Web.Facades;
using CookBook.Web.MVC.ViewModels.Recipe;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CookBook.Web.MVC.Controllers
{
    public class RecipeController : Controller
    {
        private readonly RecipeFacade _recipeFacade;
        private readonly IngredientFacade _ingredientFacade;

        public RecipeController(
            RecipeFacade recipeFacade,
            IngredientFacade ingredientFacade)
        {
            _recipeFacade = recipeFacade;
            _ingredientFacade = ingredientFacade;
        }

        public async Task<IActionResult> List()
        {
            var recipes = await _recipeFacade.GetAllAsync();
            var recipeListViewModel = new RecipeListViewModel
            {
                Recipes = recipes
            };
            return View(recipeListViewModel);
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var recipe = await _recipeFacade.GetByIdAsync(id);
            var recipeDetailViewModel = new RecipeDetailViewModel
            {
                RecipeDetail = recipe
            };
            return View(recipeDetailViewModel);
        }

        public async Task<IActionResult> New()
        {
            var recipeNewViewModel = new RecipeNewViewModel
            {
                RecipeNewModel = new RecipeNewModel()
            };
            return View(recipeNewViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(RecipeNewViewModel recipeNewViewModel)
        {
            if (TimeSpan.TryParse(recipeNewViewModel.DurationText, out TimeSpan duration))
            {
                recipeNewViewModel.RecipeNewModel.Duration = duration;
            }
            await _recipeFacade.InsertAsync(recipeNewViewModel.RecipeNewModel);
            return RedirectToAction(nameof(List));
        }
    }
}