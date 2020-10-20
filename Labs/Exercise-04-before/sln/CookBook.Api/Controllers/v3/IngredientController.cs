using CookBook.BL.Api.Facades;
using CookBook.Common.Resources;
using CookBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CookBook.Api.Controllers.v3
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("3.0")]
    public class IngredientController : ControllerBase
    {
        private const string ApiOperationBaseName = "Ingredient";
        private readonly IngredientFacade ingredientFacade;
        private readonly IStringLocalizer<IngredientControllerResources> ingredientControllerLocalizer;

        public IngredientController(
            IngredientFacade ingredientFacade,
            IStringLocalizer<IngredientControllerResources> ingredientControllerLocalizer)
        {
            this.ingredientFacade = ingredientFacade;
            this.ingredientControllerLocalizer = ingredientControllerLocalizer;
        }

        [HttpGet]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetAll))]
        public ActionResult<List<IngredientListModel>> GetAll()
        {
            return ingredientFacade.GetAll().ToList();
        }

        [HttpGet("{id}")]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetById))]
        public ActionResult<IngredientDetailModel> GetById(Guid id)
        {
            var ingredient = ingredientFacade.GetById(id);
            if (ingredient == null)
            {
                return NotFound(ingredientControllerLocalizer[nameof(IngredientControllerResources.GetById_NotFound), id].Value);
            }

            return ingredient;
        }

        [HttpPost]
        [OpenApiOperation(ApiOperationBaseName + nameof(Create))]
        public ActionResult<Guid> Create(IngredientDetailModel ingredient)
        {
            return ingredientFacade.Create(ingredient);
        }

        [HttpPut]
        [OpenApiOperation(ApiOperationBaseName + nameof(Update))]
        public ActionResult<Guid> Update(IngredientDetailModel ingredient)
        {
            return ingredientFacade.Update(ingredient);
        }

        [HttpDelete("{id}")]
        [OpenApiOperation(ApiOperationBaseName + nameof(Delete))]
        public IActionResult Delete(Guid id)
        {
            ingredientFacade.Delete(id);
            return Ok();
        }
    }
}