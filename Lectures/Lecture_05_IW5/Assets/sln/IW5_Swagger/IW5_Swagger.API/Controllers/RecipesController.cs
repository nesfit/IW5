using System;
using System.Collections.Generic;
using System.Linq;
using IW5_Swagger.API.Models;
using IW5_Swagger.API.Storage;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IW5_Swagger.API.Controllers
{
    [Route("/api/recipes")]
    public class RecipesController : Controller
    {
        [HttpGet]
        [Route("items")]
        [SwaggerOperation(OperationId = "RecipesGetAllItems")]
        public ActionResult<List<RecipeModel>> GetAllItems()
        {
            return RecipesStorage.Recipes;
        }

        [HttpGet]
        [Route("item")]
        [SwaggerOperation(OperationId = "RecipesGetItem")]
        public ActionResult<RecipeModel> GetItem(Guid id)
        {
            return RecipesStorage.Recipes.FirstOrDefault(recipe => recipe.Id == id);
        }

        [HttpPost]
        [Route("item")]
        [SwaggerOperation(OperationId = "RecipesInsertItem")]
        public ActionResult InsertItem(RecipeModel recipe)
        {
            RecipesStorage.Recipes.Add(recipe);
            return Ok();
        }
    }
}