using System;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Models.Ingredient
{
    public class IngredientDetailModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }
    }
}