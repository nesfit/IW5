using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CookBook.DAL.Entities.Base.Implementation;

namespace CookBook.DAL.Entities
{
    public class RecipeEntity : EntityBase
    {
        [Required]
        public string Name { get; set; }
        public FoodType Type { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public virtual ICollection<IngredientAmountEntity> Ingredients { get; set; } = new List<IngredientAmountEntity>();
    }
}