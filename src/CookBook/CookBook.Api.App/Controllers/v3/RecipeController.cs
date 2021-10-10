using System;
using System.Collections.Generic;
using CookBook.Api.BL.Facades;
using CookBook.Common.Models;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace CookBook.Api.App.Controllers.v3
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("3.0")]
    public class RecipeController : ControllerBase
    {
        private const string ApiOperationBaseName = "Recipe";
        private readonly IRecipeFacade recipeFacade;

        public RecipeController(IRecipeFacade recipeFacade)
        {
            this.recipeFacade = recipeFacade;
        }

        [HttpGet]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetAll))]
        public ActionResult<List<RecipeListModel>> GetAll()
        {
            return recipeFacade.GetAll();
        }

        [HttpGet("{id:guid}")]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetById))]
        public ActionResult<RecipeDetailModel?> GetById(Guid id)
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

        [HttpPost("upsert")]
        [OpenApiOperation(ApiOperationBaseName + nameof(CreateOrUpdate))]
        public ActionResult<Guid> CreateOrUpdate(RecipeDetailModel recipe)
        {
            return recipeFacade.CreateOrUpdate(recipe);
        }


        [HttpDelete("{id:guid}")]
        [OpenApiOperation(ApiOperationBaseName + nameof(Delete))]
        public IActionResult Delete(Guid id)
        {
            recipeFacade.Delete(id);
            return Ok();
        }
    }
}
