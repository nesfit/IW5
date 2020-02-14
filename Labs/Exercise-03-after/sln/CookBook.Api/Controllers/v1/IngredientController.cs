using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System;

namespace CookBook.Api.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class IngredientController : ControllerBase
    {
        [HttpGet]
        [OpenApiOperation("Ingredient" + nameof(GetAll))]
        public IActionResult GetAll()
        {
            var ingredients = new[]
            {
                new { Id = new Guid("df935095-8709-4040-a2bb-b6f97cb416dc"), Name = "Vejce", Description = "Popis vajec" },
                new { Id = new Guid("23b3902d-7d4f-4213-9cf0-112348f56238"), Name = "Name 2", Description = "Description 2" }
            };
            return Ok(ingredients);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            if (id == new Guid("df935095-8709-4040-a2bb-b6f97cb416dc"))
            {
                return Ok(new
                {
                    Id = new Guid("df935095-8709-4040-a2bb-b6f97cb416dc"),
                    Name = "Vejce"
                });
            }
            else
            {
                return NotFound();
            }
        }
    }
}