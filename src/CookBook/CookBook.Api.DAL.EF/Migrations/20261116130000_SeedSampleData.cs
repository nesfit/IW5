using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.Api.DAL.EF.Migrations
{
    [DbContext(typeof(CookBookDbContext))]
    [Migration("20261116130000_SeedSampleData")]
    public class SeedSampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "Description", "ImageUrl" },
                columnTypes: new[] { "uniqueidentifier", "nvarchar(max)", "nvarchar(max)", "nvarchar(max)" },
                values: new object[,]
                {
                    { new Guid("df935095-8709-4040-a2bb-b6f97cb416dc"), "Vejce", "Popis vejce", "https://i.ibb.co/d7mZWGN/image.jpg" },
                    { new Guid("23b3902d-7d4f-4213-9cf0-112348f56238"), "Cibule", "Popis cibule", "https://i.ibb.co/sbXC0rS/480px-Onion-on-White.jpg" },
                    { new Guid("7f251cd6-3ac4-49be-b3e7-d1f9f7cfdd3a"), "Slanina", "Popis slaniny", null },
                    { new Guid("adb7daf1-8a6c-4cbb-b4f5-631a9b7f7287"), "Rajče", "Popis rajčete", "https://i.ibb.co/1TzsF6B/ingredient-7.jpg" },
                    { new Guid("a8978e5d-0c5b-449c-9dc0-0a01563c0c3b"), "Mléko", "Popis mléka", "https://i.ibb.co/BB3gVxr/ingredient-2.jpg" },
                    { new Guid("0e88301e-cd92-47cf-8ee7-5cb0752e9f82"), "Rýže", "Popis rýže", "https://i.ibb.co/98CGN3H/ingredient-3.jpg" },
                    { new Guid("e79f129f-3153-41df-8e84-8bcd7a077648"), "Citron", "Popis citronu", "https://i.ibb.co/0KQgsdT/ingredient-4.jpg" },
                    { new Guid("a62a2fb6-2b80-45b1-8f82-1401a6834abe"), "Kuřecí maso", "Popis kuřecího masa", "https://i.ibb.co/4KVB05k/ingredient-5.jpg" },
                    { new Guid("78c2a34b-1e84-40c8-bc59-49510478679d"), "Chilli paprička", "Popis chilli papričky", "https://i.ibb.co/VDB2bQT/ingredient-6.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Name", "Description", "Duration", "FoodType", "ImageUrl" },
                columnTypes: new[] { "uniqueidentifier", "nvarchar(max)", "nvarchar(max)", "time", "int", "nvarchar(max)" },
                values: new object[,]
                {
                    { new Guid("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"), "Míchaná vejce", "Popis míchaných vajec", new TimeSpan(0, 15, 0), 1, "https://i.ibb.co/mJgrX6B/Scrambled-eggs-01.jpg" },
                    { new Guid("a8ee7ce8-9903-4f42-afb4-b2c34dfb7ccf"), "Miso polévka", "Polévka!", new TimeSpan(1, 0, 0), 2, "https://i.ibb.co/RY1XKmL/recipe-2.jpg" },
                    { new Guid("c3542130-589c-4302-a441-a110fcadd45a"), "Vykoštěné kuře s citronem a bylinkami", "Popis kuřete", new TimeSpan(1, 0, 0), 1, "https://i.ibb.co/QJF2ZxX/recipe-1.jpg" },
                    { new Guid("2caa29d8-61f0-4c1d-850d-4d70003e6aef"), "Citronový sorbet", "Zákusek", new TimeSpan(0, 30, 0), 3, null }
                });

            migrationBuilder.InsertData(
                table: "IngredientAmounts",
                columns: new[] { "Id", "Amount", "Unit", "RecipeId", "IngredientId" },
                columnTypes: new[] { "uniqueidentifier", "float", "int", "uniqueidentifier", "uniqueidentifier" },
                values: new object[,]
                {
                    { new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5e"), 4.0, 5, new Guid("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"), new Guid("df935095-8709-4040-a2bb-b6f97cb416dc") },
                    { new Guid("87833e66-05ba-4d6b-900b-fe5ace88dbd8"), 1.0, 5, new Guid("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"), new Guid("23b3902d-7d4f-4213-9cf0-112348f56238") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IngredientAmounts",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValues: new object[]
                {
                    new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
                    new Guid("87833e66-05ba-4d6b-900b-fe5ace88dbd8")
                });

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValues: new object[]
                {
                    new Guid("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
                    new Guid("a8ee7ce8-9903-4f42-afb4-b2c34dfb7ccf"),
                    new Guid("c3542130-589c-4302-a441-a110fcadd45a"),
                    new Guid("2caa29d8-61f0-4c1d-850d-4d70003e6aef")
                });

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValues: new object[]
                {
                    new Guid("df935095-8709-4040-a2bb-b6f97cb416dc"),
                    new Guid("23b3902d-7d4f-4213-9cf0-112348f56238"),
                    new Guid("7f251cd6-3ac4-49be-b3e7-d1f9f7cfdd3a"),
                    new Guid("adb7daf1-8a6c-4cbb-b4f5-631a9b7f7287"),
                    new Guid("a8978e5d-0c5b-449c-9dc0-0a01563c0c3b"),
                    new Guid("0e88301e-cd92-47cf-8ee7-5cb0752e9f82"),
                    new Guid("e79f129f-3153-41df-8e84-8bcd7a077648"),
                    new Guid("a62a2fb6-2b80-45b1-8f82-1401a6834abe"),
                    new Guid("78c2a34b-1e84-40c8-bc59-49510478679d")
                });
        }
    }
}
