using CookBook.DAL.Entities.Base.Implementation;
using System.ComponentModel.DataAnnotations;

namespace CookBook.DAL.Entities
{
    public class IngredientAmountEntity : EntityBase
    {
        [Required]
        public double Amount { get; set; }
        [Required]
        public Unit Unit { get; set; }
        [Required]
        public IngredientEntity Ingredient { get; set; }
    }
}
