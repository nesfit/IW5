using System;
using System.Collections.Generic;
using CookBook.DAL.Entities;

namespace CookBook.BL.Models
{
    public class RecipeDetailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public FoodType Type { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public virtual ICollection<IngredientAmountEntity> Ingredients { get; set; }
    }
}
