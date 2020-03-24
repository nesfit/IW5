using CookBook.BL.Web.Facades;
using Microsoft.AspNetCore.Mvc;
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
            return View(ingredients);
        }
    }
}