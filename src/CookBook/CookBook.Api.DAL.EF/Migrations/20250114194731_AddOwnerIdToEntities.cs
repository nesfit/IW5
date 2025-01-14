using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.Api.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerIdToEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "IngredientAmounts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "IngredientAmounts");
        }
    }
}
