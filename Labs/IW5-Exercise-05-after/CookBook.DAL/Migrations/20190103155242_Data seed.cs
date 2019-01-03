using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CookBook.DAL.Migrations
{
    public partial class Dataseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("5abdfee1-c970-4afd-aff8-aa3cfef8b1ac"), "Tmavá čokoláda s 80% kakaa.", "Tmavá čokoláda" },
                    { new Guid("83041385-cb60-401b-bf11-cc5ffb8bc570"), "Plnotučné mlieko so 4% tuku.", "Plnotučné mlieko" },
                    { new Guid("cb181669-4e02-449f-bf02-ab6020dfecb4"), "Najemno umletá mandlová múka", "Mandlová múka" },
                    { new Guid("012ac89a-94e3-4bc2-94b5-c9b05fc83375"), "Slepačie vajíčko.", "Vajíčko" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Description", "Duration", "Name", "Type" },
                values: new object[] { new Guid("cb8db9b3-799c-4ef2-9d85-ce32a9ffa843"), "Hovězí maso dobře nasolíme a obalíme v zázvoru, pak v hladké mouce a necháme asi hodinu odležet. V hrnci si osmažíme na pokrájeném špeku s trochou oleje cibuli dosklovata. Pak vložíme hovězí maso, které ze všech stran osmažíme dozlatova. Ke konci smažení přidáme orestovat buď plátky čerstvých hub (200 g) nebo přidáme hrst předem namočených sušených hub a vše zalijeme vývarem nebo vodou s kostkou bujonu. Dusíme pod poklicí, než bude maso měkké, dle potřeby podléváme a občas maso otočíme.", new TimeSpan(0, 0, 30, 0, 0), "Čokoládová torta", 2 });

            migrationBuilder.InsertData(
                table: "IngredientAmounts",
                columns: new[] { "Id", "Amount", "IngredientId", "RecipeId", "Unit" },
                values: new object[,]
                {
                    { new Guid("1d2e7873-3e35-4d40-877c-a3d0d78de3c0"), 0.5, new Guid("5abdfee1-c970-4afd-aff8-aa3cfef8b1ac"), new Guid("cb8db9b3-799c-4ef2-9d85-ce32a9ffa843"), 1 },
                    { new Guid("2711f535-3566-446c-9ac6-58261efe3fa3"), 0.29999999999999999, new Guid("83041385-cb60-401b-bf11-cc5ffb8bc570"), new Guid("cb8db9b3-799c-4ef2-9d85-ce32a9ffa843"), 2 },
                    { new Guid("c8cdbff9-6692-42ad-93aa-69cb56f95019"), 5.0, new Guid("cb181669-4e02-449f-bf02-ab6020dfecb4"), new Guid("cb8db9b3-799c-4ef2-9d85-ce32a9ffa843"), 6 },
                    { new Guid("b417ad46-b94c-487e-8cc1-97ebd7551b13"), 7.0, new Guid("012ac89a-94e3-4bc2-94b5-c9b05fc83375"), new Guid("cb8db9b3-799c-4ef2-9d85-ce32a9ffa843"), 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IngredientAmounts",
                keyColumn: "Id",
                keyValue: new Guid("1d2e7873-3e35-4d40-877c-a3d0d78de3c0"));

            migrationBuilder.DeleteData(
                table: "IngredientAmounts",
                keyColumn: "Id",
                keyValue: new Guid("2711f535-3566-446c-9ac6-58261efe3fa3"));

            migrationBuilder.DeleteData(
                table: "IngredientAmounts",
                keyColumn: "Id",
                keyValue: new Guid("b417ad46-b94c-487e-8cc1-97ebd7551b13"));

            migrationBuilder.DeleteData(
                table: "IngredientAmounts",
                keyColumn: "Id",
                keyValue: new Guid("c8cdbff9-6692-42ad-93aa-69cb56f95019"));

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: new Guid("012ac89a-94e3-4bc2-94b5-c9b05fc83375"));

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: new Guid("5abdfee1-c970-4afd-aff8-aa3cfef8b1ac"));

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: new Guid("83041385-cb60-401b-bf11-cc5ffb8bc570"));

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: new Guid("cb181669-4e02-449f-bf02-ab6020dfecb4"));

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("cb8db9b3-799c-4ef2-9d85-ce32a9ffa843"));
        }
    }
}
