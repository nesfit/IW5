using CookBook.BL.Api.Facades;
using CookBook.BL.Api.Models.Recipe;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System;
using System.Collections.Generic;

namespace CookBook.Api.Controllers.v3
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("3.0")]
    public class RecipeController : ControllerBase
    {
        private const string ApiOperationBaseName = "Recipe";
        private readonly RecipeFacade recipeFacade;

        public RecipeController(RecipeFacade recipeFacade)
        {
            this.recipeFacade = recipeFacade;
        }

        [HttpGet]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetAll))]
        public ActionResult<List<RecipeListModel>> GetAll()
        {
            return recipeFacade.GetAll();
        }

        [HttpGet("{id}")]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetById))]
        public ActionResult<RecipeDetailModel> GetById(Guid id)
        {
            return recipeFacade.GetById(id);
        }

        [HttpPost]
        [OpenApiOperation(ApiOperationBaseName + nameof(Create))]
        public ActionResult<Guid> Create(RecipeDetailModel recipe)
        {
            return recipeFacade.Create(recipe);
        }

        [HttpPut]
        [OpenApiOperation(ApiOperationBaseName + nameof(Update))]
        public ActionResult<Guid?> Update(RecipeDetailModel recipe)
        {
            return recipeFacade.Update(recipe);
        }

        [HttpDelete("{id}")]
        [OpenApiOperation(ApiOperationBaseName + nameof(Delete))]
        public IActionResult Delete(Guid id)
        {
            recipeFacade.Delete(id);
            return Ok();
        }
    }
}