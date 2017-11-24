using System;
using CookBook.BL.Models;

namespace CookBook.BL.Messages
{
    public class UpdatedRecipeMessage 
    {
        public RecipeDetailModel Model { get; set; }
        public UpdatedRecipeMessage(RecipeDetailModel model)
        {
            Model = model;
        }


    }
}