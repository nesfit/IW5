namespace CookBook.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPropertiesIngredientEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IngredientEntities", "Description", c => c.String());
            AddColumn("dbo.IngredientEntities", "Argb", c => c.Int(nullable: false));
            AlterColumn("dbo.IngredientEntities", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IngredientEntities", "Name", c => c.String());
            DropColumn("dbo.IngredientEntities", "Argb");
            DropColumn("dbo.IngredientEntities", "Description");
        }
    }
}
