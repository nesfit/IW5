using CookBook.DAL.Entities;
using CookBook.Models;
using System;
using System.Collections.Generic;

namespace CookBook.DAL
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
            Ingredients.Add(new IngredientEntity
            {
                Id = ingredientGuids[0],
                Name = "Vejce",
                Description = "Popis vajec"
            });
            Ingredients.Add(new IngredientEntity
            {
                Id = ingredientGuids[1],
                Name = "Cibule",
                Description = "Popis cibule"
            });
        }

        private void SeedIngredientAmounts()
        {
            IngredientAmounts.Add(new IngredientAmountEntity
            {
                Id = ingredientAmountGuids[0],
                Amount = 4.0,
                Unit = Unit.Pieces,
                IngredientId = ingredientGuids[0],
                RecipeId = recipeGuids[0]
            });

            IngredientAmounts.Add(new IngredientAmountEntity
            {
                Id = ingredientAmountGuids[1],
                Amount = 1.0,
                Unit = Unit.Pieces,
                IngredientId = ingredientGuids[1],
                RecipeId = recipeGuids[0]
            });
        }

        private void SeedRecipes()
        {
            Recipes.Add(new RecipeEntity
            {
                Id = recipeGuids[0],
                Name = "Míchaná vejce",
                Description = "Popis míchaných vajec",
                Duration = TimeSpan.FromMinutes(15),
                FoodType = FoodType.MainDish
            });
        }
    }
}