namespace CookBook.DAL.Migrations
{
    using Entities;
    using System;
    using System.Data.Entity.Migrations;
    using System.Drawing;

    internal sealed class Configuration : DbMigrationsConfiguration<CookBookDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CookBookDbContext context)
        {
            var darkChocolate = new IngredientEntity
            {
                Id = new Guid("5abdfee1-c970-4afd-aff8-aa3cfef8b1ac"),
                Name = "Tmavá čokoláda",
                Description = "Tmavá čokoláda s 80% kakaa.",
            };

            var wholeMilk = new IngredientEntity
            {
                Id = new Guid("83041385-cb60-401b-bf11-cc5ffb8bc570"),
                Name = "Plnotučné mlieko",
                Description = "Plnotučné mlieko so 4% tuku.",
            };

            var almondFlour = new IngredientEntity
            {
                Id = new Guid("cb181669-4e02-449f-bf02-ab6020dfecb4"),
                Name = "Mandlová múka",
                Description = "Najemno umletá mandlová múka",
            };

            var egg = new IngredientEntity
            {
                Id = new Guid("012ac89a-94e3-4bc2-94b5-c9b05fc83375"),
                Name = "Vajíčko",
                Description = "Slepačie vajíčko.",
            };

            context.Ingredients.AddOrUpdate(i => i.Id, wholeMilk, darkChocolate, almondFlour, egg);

            var chocolateCakeId = new Guid("cb8db9b3-799c-4ef2-9d85-ce32a9ffa843");
            context.Recipes.AddOrUpdate(
                r => r.Id,
                new RecipeEntity
                {
                    Id = chocolateCakeId,
                    Name = "Čokoládová torta",
                    Duration = TimeSpan.FromMinutes(30),
                    Type = FoodType.Dessert,
                    Description = "Hovězí maso dobře nasolíme a obalíme v zázvoru, pak v hladké mouce a necháme asi hodinu odležet. " +
                                  "V hrnci si osmažíme na pokrájeném špeku s trochou oleje cibuli dosklovata. Pak vložíme hovězí maso, " +
                                  "které ze všech stran osmažíme dozlatova. Ke konci smažení přidáme orestovat buď plátky čerstvých " +
                                  "hub (200 g) nebo přidáme hrst předem namočených sušených hub a vše zalijeme vývarem nebo vodou " +
                                  "s kostkou bujonu. Dusíme pod poklicí, než bude maso měkké, dle potřeby podléváme a občas maso otočíme.",
                    Ingredients =
                    {
                        new IngredientAmountEntity
                        {
                            Id = new Guid("1d2e7873-3e35-4d40-877c-a3d0d78de3c0"),
                            Amount = 0.5,
                            Unit = Unit.Kg,
                            RecipeId = chocolateCakeId,
                            IngredientId = darkChocolate.Id
                        },
                        new IngredientAmountEntity
                        {
                            Id = new Guid("2711f535-3566-446c-9ac6-58261efe3fa3"),
                            Amount = 0.3,
                            Unit = Unit.L,
                            RecipeId = chocolateCakeId,
                            IngredientId = wholeMilk.Id
                        },
                        new IngredientAmountEntity
                        {
                            Id = new Guid("c8cdbff9-6692-42ad-93aa-69cb56f95019"),
                            Amount = 5,
                            Unit = Unit.Pieces,
                            RecipeId = chocolateCakeId,
                            IngredientId = egg.Id
                        },
                        new IngredientAmountEntity
                        {
                            Id = new Guid("b417ad46-b94c-487e-8cc1-97ebd7551b13"),
                            Amount = 7,
                            Unit = Unit.Spoon,
                            RecipeId = chocolateCakeId,
                            IngredientId = almondFlour.Id
                        }
                    }
                });

            context.SaveChanges();
        }
    }
}
