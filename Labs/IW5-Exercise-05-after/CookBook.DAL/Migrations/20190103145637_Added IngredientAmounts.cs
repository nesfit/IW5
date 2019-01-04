using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CookBook.DAL.Migrations
{
    public partial class AddedIngredientAmounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Recipes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "IngredientAmounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Unit = table.Column<int>(nullable: false),
                    IngredientId = table.Column<Guid>(nullable: false),
                    RecipeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientAmounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientAmounts_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientAmounts_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientAmounts_IngredientId",
                table: "IngredientAmounts",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientAmounts_RecipeId",
                table: "IngredientAmounts",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientAmounts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Recipes",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
