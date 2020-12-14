using CookBook.DAL.Entities;
using CookBook.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System;
using System.Collections.Generic;

namespace CookBook.Api.Controllers.v2
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("2.0")]
    public class IngredientController : ControllerBase
    {
        private readonly IngredientRepository ingredientRepository;

        public IngredientController(IngredientRepository ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }

        [HttpGet]
        [OpenApiOperation("Ingredient" + nameof(GetAll))]
        public IList<IngredientEntity> GetAll()
        {
            return ingredientRepository.GetAll();
        }

        [HttpGet("{id}")]
        [OpenApiOperation("Ingredient" + nameof(GetById))]
        public IngredientEntity GetById(Guid id)
        {
            return ingredientRepository.GetById(id);
        }
    }
}