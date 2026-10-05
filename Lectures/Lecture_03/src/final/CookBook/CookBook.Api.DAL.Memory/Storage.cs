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
            new("df935095-8709-4040-a2bb-b6f97cb416dc"),
            new("23b3902d-7d4f-4213-9cf0-112348f56238"),
            new("7f251cd6-3ac4-49be-b3e7-d1f9f7cfdd3a"),
            new("adb7daf1-8a6c-4cbb-b4f5-631a9b7f7287"),
            new("a8978e5d-0c5b-449c-9dc0-0a01563c0c3b"),
            new("0e88301e-cd92-47cf-8ee7-5cb0752e9f82"),
            new("e79f129f-3153-41df-8e84-8bcd7a077648"),
            new("a62a2fb6-2b80-45b1-8f82-1401a6834abe"),
            new("78c2a34b-1e84-40c8-bc59-49510478679d"),
        };

        private readonly IList<Guid> ingredientAmountGuids = new List<Guid>
        {
            new("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
            new("87833e66-05ba-4d6b-900b-fe5ace88dbd8"),
        };

        private readonly IList<Guid> recipeGuids = new List<Guid>
        {
            new("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
            new("a8ee7ce8-9903-4f42-afb4-b2c34dfb7ccf"),
            new("c3542130-589c-4302-a441-a110fcadd45a"),
            new("2caa29d8-61f0-4c1d-850d-4d70003e6aef"),
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
        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[0],
            Name = "Vejce",
            Description = "Popis vejce",
            ImageUrl = "https://i.ibb.co/d7mZWGN/image.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[1],
            Name = "Cibule",
            Description = "Popis cibule",
            ImageUrl = "https://i.ibb.co/sbXC0rS/480px-Onion-on-White.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[2],
            Name = "Slanina",
            Description = "Popis slaniny"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[3],
            Name = "Rajče",
            Description = "Popis rajčete",
            ImageUrl = "https://i.ibb.co/1TzsF6B/ingredient-7.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[4],
            Name = "Mléko",
            Description = "Popis mléka",
            ImageUrl = "https://i.ibb.co/BB3gVxr/ingredient-2.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[5],
            Name = "Rýže",
            Description = "Popis rýže",
            ImageUrl = "https://i.ibb.co/98CGN3H/ingredient-3.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[6],
            Name = "Citron",
            Description = "Popis citronu",
            ImageUrl = "https://i.ibb.co/0KQgsdT/ingredient-4.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[7],
            Name = "Kuřecí maso",
            Description = "Popis kuřecího masa",
            ImageUrl = "https://i.ibb.co/4KVB05k/ingredient-5.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[8],
            Name = "Chilli paprička",
            Description = "Popis chilli papričky",
            ImageUrl = "https://i.ibb.co/VDB2bQT/ingredient-6.jpg"
        });
    }

    private void SeedIngredientAmounts()
    {
        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[0],
            Amount = 4.0,
            Unit = Unit.Pieces,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[0]
        });

        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[1],
            Amount = 1.0,
            Unit = Unit.Pieces,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[1]
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
            FoodType = FoodType.MainDish,
            ImageUrl = "https://i.ibb.co/mJgrX6B/Scrambled-eggs-01.jpg"
        });

        Recipes.Add(new RecipeEntity
        {
            Id = recipeGuids[1],
            Name = "Miso polévka",
            Duration = TimeSpan.FromMinutes(60),
            FoodType = FoodType.Soup,
            Description = "Polévka!",
            ImageUrl = "https://i.ibb.co/RY1XKmL/recipe-2.jpg",
        });


        Recipes.Add(new RecipeEntity
        {
            Id = recipeGuids[2],
            Name = "Vykoštěné kuře s citronem a bylinkami",
            Duration = TimeSpan.FromMinutes(60),
            FoodType = FoodType.MainDish,
            Description = "Popis kuřete",
            ImageUrl = "https://i.ibb.co/QJF2ZxX/recipe-1.jpg",
        });

        Recipes.Add(new RecipeEntity
        {
            Id = recipeGuids[3],
            Name = "Citronový sorbet",
            Duration = TimeSpan.FromMinutes(30),
            FoodType = FoodType.Dessert,
            Description = "Zákusek",
        });
    }
    }
}
