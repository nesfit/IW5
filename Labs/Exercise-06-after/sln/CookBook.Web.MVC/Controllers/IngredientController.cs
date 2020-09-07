using CookBook.BL.Web.MVC.Api;
using CookBook.BL.Web.MVC.Facades;
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

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var ingredients = await _ingredientFacade.GetAllAsync();
            var ingredientListViewModel = new IngredientListViewModel
            {
                Ingredients = ingredients
            };
            return View(ingredientListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var ingredient = await _ingredientFacade.GetByIdAsync(id);
            var ingredientDetailViewModel = new IngredientDetailViewModel
            {
                IngredientDetail = ingredient
            };
            return View(ingredientDetailViewModel);
        }

        [HttpGet]
        public IActionResult New()
        {
            var ingredientNewViewModel = new IngredientNewViewModel
            {
                IngredientModel = new IngredientDetailModel()
            };
            return View(ingredientNewViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(IngredientNewViewModel ingredientNewViewModel)
        {
            await _ingredientFacade.InsertAsync(ingredientNewViewModel.IngredientModel);
            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid ingredientId)
        {
            await _ingredientFacade.DeleteAsync(ingredientId);
            return RedirectToAction(nameof(List));
        }
    }
}