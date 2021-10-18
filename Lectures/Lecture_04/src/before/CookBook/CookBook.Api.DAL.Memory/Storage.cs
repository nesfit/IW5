using System;
using System.Collections.Generic;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Common.Enums;

namespace CookBook.Api.DAL.Memory
{
    public class Storage
    {
        private readonly IList<Guid> ingredientGuids = new List<Guid>
        {
            new ("df935095-8709-4040-a2bb-b6f97cb416dc"),
            new ("23b3902d-7d4f-4213-9cf0-112348f56238")
        };

        private readonly IList<Guid> ingredientAmountGuids = new List<Guid>
        {
            new ("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
            new ("87833e66-05ba-4d6b-900b-fe5ace88dbd8")
        };

        private readonly IList<Guid> recipeGuids = new List<Guid>
        {
            new ("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e")
        };

        public IList<IngredientEntity> Ingredients { get; } = new List<IngredientEntity>();
        public IList<IngredientAmountEntity> IngredientAmounts { get; } = new List<IngredientAmountEntity>();
        public IList<RecipeEntity> Recipes { get; } = new List<RecipeEntity>();

        public Storage(bool seedData = true)
        {
            if (seedData)
            {
                SeedIngredients();
                SeedIngredientAmounts();
                SeedRecipes();
            }
        }

        private void SeedIngredients()
        {
            Ingredients.Add(new IngredientEntity(ingredientGuids[0], "Vejce", "Popis vajec", "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Chicken_egg_2009-06-04.jpg/428px-Chicken_egg_2009-06-04.jpg"));
            Ingredients.Add(new IngredientEntity(ingredientGuids[1], "Cibule", "Popis cibule", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/25/Onion_on_White.JPG/480px-Onion_on_White.JPG"));
        }

        private void SeedIngredientAmounts()
        {
            IngredientAmounts.Add(new IngredientAmountEntity(ingredientAmountGuids[0], 4.0, Unit.Pieces, recipeGuids[0], ingredientGuids[0])
            {
                Id = ingredientAmountGuids[0]
            });

            IngredientAmounts.Add(new IngredientAmountEntity(ingredientAmountGuids[1], 1.0, Unit.Pieces, recipeGuids[0], ingredientGuids[1])
            {
                Id = ingredientAmountGuids[1]
            });
        }

        private void SeedRecipes()
        {
            Recipes.Add(new RecipeEntity(
                "Míchaná vejce", 
                "Popis míchaných vajec", 
                TimeSpan.FromMinutes(15), 
                FoodType.MainDish,
                "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ef/Scrambled_eggs-01.jpg/320px-Scrambled_eggs-01.jpg")
            {
                Id = recipeGuids[0]
            });
        }
    }
}
