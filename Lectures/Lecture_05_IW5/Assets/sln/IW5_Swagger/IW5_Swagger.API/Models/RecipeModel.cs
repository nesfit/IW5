using System;
using IW5_Swagger.API.Enums;

namespace IW5_Swagger.API.Models
{
    public class RecipeModel : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public FoodType FoodType { get; set; }
    }
}