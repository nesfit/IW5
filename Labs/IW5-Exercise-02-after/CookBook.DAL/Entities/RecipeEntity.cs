using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookBook.DAL.Entities
{
    public class RecipeEntity : EntityBase
    {
        [Required]
        public string Name { get; set; }
        public FoodType Type { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        [Required]
        public virtual ICollection<IngredientEntityAmount> Ingredients { get; set; } = new List<IngredientEntityAmount>();
    }
}
