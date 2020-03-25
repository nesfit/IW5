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

        public RecipeController(
            RecipeFacade recipeFacade)
        {
            _recipeFacade = recipeFacade;
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
    }
}