using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CookBook.Common.Enums;
using CookBook.Common.Extensions;

namespace CookBook.Common.Models
{
    public class RecipeDetailIngredientModel : IValidatableObject
    {
        public Guid? Id { get; set; }

        [Range(0, double.MaxValue)]
        public double Amount { get; set; }
        public Unit Unit { get; set; }
        public IngredientListModel Ingredient { get; set; }
        public Guid IngredientId { get; set; }
        public string UnitDisplayText => Unit.GetReadableName();

        public RecipeDetailIngredientModel()
        {

        }

        public RecipeDetailIngredientModel(Guid? id, double amount, Unit unit, IngredientListModel ingredient)
        {
            Id = id;
            Amount = amount;
            Unit = unit;
            Ingredient = ingredient;
            IngredientId = ingredient.Id;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Unit == Unit.Unknown)
            {
                yield return new ValidationResult("Unit is required!", new[] { nameof(Unit) });
            }
            if (IngredientId == Guid.Empty)
            {
                yield return new ValidationResult("Ingredient is required!", new[] { nameof(IngredientId) });
            }
        }
    }
}
