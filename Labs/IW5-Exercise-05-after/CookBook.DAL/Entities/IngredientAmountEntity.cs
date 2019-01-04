using CookBook.DAL.Entities.Base.Implementation;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.DAL.Entities
{
    [Table("IngredientAmounts")]
    public class IngredientAmountEntity : EntityBase
    {
        public double Amount { get; set; }
        public Unit Unit { get; set; }

        [ForeignKey(nameof(Ingredient))]
        public Guid IngredientId { get; set; }
        public virtual IngredientEntity Ingredient { get; set; }

        [ForeignKey(nameof(Recipe))]
        public Guid RecipeId { get; set; }
        public virtual RecipeEntity Recipe { get; set; }
    }
}