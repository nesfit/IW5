using CookBook.DAL.Entities.Base.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.DAL.Entities
{
    [Table("Recipes")]
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