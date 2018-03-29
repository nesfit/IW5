using System;
using System.Collections.Generic;
using CookBook.DAL.Entities;

namespace CookBook.BL.Models
{
    public class RecipeDetailModel
    {
        public string Name { get; set; }
        public FoodType Type { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public virtual IList<IngredientListModel> Ingredients { get; set; } = new List<IngredientListModel>();
    }
}