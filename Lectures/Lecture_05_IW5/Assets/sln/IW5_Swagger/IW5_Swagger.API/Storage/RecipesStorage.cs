using System;
using System.Collections.Generic;
using IW5_Swagger.API.Enums;
using IW5_Swagger.API.Models;

namespace IW5_Swagger.API.Storage
{
    public static class RecipesStorage
    {
        public static List<RecipeModel> Recipes { get; set; } = new List<RecipeModel>
        {
            new RecipeModel
            {
                Id = new Guid("56A1B6CF-80B8-4963-9CBE-35BE4E78929A"),
                Name = "Čokoládový dort",
                Description = "Popis receptu",
                FoodType = FoodType.Dessert
            }
        };
    }
}