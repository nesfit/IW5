using System;
using System.ComponentModel.DataAnnotations;
using CookBook.DAL.Entities.Base.Implementation;

namespace CookBook.DAL.Entities
{
    public class IngredientAmountEntity : EntityBase
    {
        [Required]
        public double Amount { get; set; }
        [Required]
        public Unit Unit { get; set; }

        [Required]
        public Guid IngredientId { get; set; }
        public virtual IngredientEntity Ingredient { get; set; }

        [Required]
        public Guid RecipeId { get; set; }
        public virtual RecipeEntity Recipe { get; set; }
    }
}