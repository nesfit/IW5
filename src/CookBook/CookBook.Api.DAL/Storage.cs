using CookBook.Common.Enums;
using CookBook.DAL.Api.Entities;
using System;
using System.Collections.Generic;

namespace CookBook.DAL.Api
{
    public class Storage
    {
        private readonly IList<Guid> ingredientGuids = new List<Guid>
        {
            new Guid("df935095-8709-4040-a2bb-b6f97cb416dc"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56238")
        };

        private readonly IList<Guid> ingredientAmountGuids = new List<Guid>
        {
            new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
            new Guid("87833e66-05ba-4d6b-900b-fe5ace88dbd8")
        };

        private readonly IList<Guid> recipeGuids = new List<Guid>
        {
            new Guid("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e")
        };

        public IList<IngredientEntity> Ingredients { get; } = new List<IngredientEntity>();
        public IList<IngredientAmountEntity> IngredientAmounts { get; } = new List<IngredientAmountEntity>();
        public IList<RecipeEntity> Recipes { get; } = new List<RecipeEntity>();

        public Storage()
        {
            SeedIngredients();
            SeedIngredientAmounts();
            SeedRecipes();
        }

        private void SeedIngredients()
        {
            Ingredients.Add(new IngredientEntity(ingredientGuids[0], "Vejce", "Popis vajec"));
            Ingredients.Add(new IngredientEntity(ingredientGuids[1], "Cibule", "Popis cibule"));
        }

        private void SeedIngredientAmounts()
        {
            IngredientAmounts.Add(new IngredientAmountEntity(4.0, Unit.Pieces, ingredientGuids[0], recipeGuids[0])
            {
                Id = ingredientAmountGuids[0]
            });

            IngredientAmounts.Add(new IngredientAmountEntity(1.0, Unit.Pieces, ingredientGuids[1], recipeGuids[0])
            {
                Id = ingredientAmountGuids[1]
            });
        }

        private void SeedRecipes()
        {
            Recipes.Add(new RecipeEntity("Míchaná vejce", "Popis míchaných vajec", TimeSpan.FromMinutes(15), FoodType.MainDish)
            {
                Id = recipeGuids[0]
            });
        }
    }
}
