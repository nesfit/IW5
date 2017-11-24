namespace CookBook.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IngredientAmountEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IngredientAmountEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Double(nullable: false),
                        Unit = c.Int(nullable: false),
                        Ingredient_Id = c.Guid(nullable: false),
                        RecipeEntity_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IngredientEntities", t => t.Ingredient_Id, cascadeDelete: true)
                .ForeignKey("dbo.RecipeEntities", t => t.RecipeEntity_Id, cascadeDelete: true)
                .Index(t => t.Ingredient_Id)
                .Index(t => t.RecipeEntity_Id);
            
            AlterColumn("dbo.RecipeEntities", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngredientAmountEntities", "RecipeEntity_Id", "dbo.RecipeEntities");
            DropForeignKey("dbo.IngredientAmountEntities", "Ingredient_Id", "dbo.IngredientEntities");
            DropIndex("dbo.IngredientAmountEntities", new[] { "RecipeEntity_Id" });
            DropIndex("dbo.IngredientAmountEntities", new[] { "Ingredient_Id" });
            AlterColumn("dbo.RecipeEntities", "Name", c => c.String());
            DropTable("dbo.IngredientAmountEntities");
        }
    }
}
