namespace CookBook.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrepareForExcercise3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.IngredientAmountEntities", name: "RecipeEntity_Id", newName: "RecipeId");
            RenameColumn(table: "dbo.IngredientAmountEntities", name: "Ingredient_Id", newName: "IngredientId");
            RenameIndex(table: "dbo.IngredientAmountEntities", name: "IX_Ingredient_Id", newName: "IX_IngredientId");
            RenameIndex(table: "dbo.IngredientAmountEntities", name: "IX_RecipeEntity_Id", newName: "IX_RecipeId");
            DropColumn("dbo.IngredientEntities", "Argb");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IngredientEntities", "Argb", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.IngredientAmountEntities", name: "IX_RecipeId", newName: "IX_RecipeEntity_Id");
            RenameIndex(table: "dbo.IngredientAmountEntities", name: "IX_IngredientId", newName: "IX_Ingredient_Id");
            RenameColumn(table: "dbo.IngredientAmountEntities", name: "IngredientId", newName: "Ingredient_Id");
            RenameColumn(table: "dbo.IngredientAmountEntities", name: "RecipeId", newName: "RecipeEntity_Id");
        }
    }
}
