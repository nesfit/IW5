using System;
using CookBook.BL.Models;

namespace CookBook.BL.Messages
{
    public class DeletedRecipeMessage 
    {
        public DeletedRecipeMessage(Guid recipeId)
        {
            RecipeId = recipeId;
        }

        public Guid RecipeId { get; set; }
    }
}