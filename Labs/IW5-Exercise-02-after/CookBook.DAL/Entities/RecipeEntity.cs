using System;

namespace CookBook.DAL.Entities
{
    public class RecipeEntity : EntityBase
    {
        public string Name { get; set; }
        public FoodType Type { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
