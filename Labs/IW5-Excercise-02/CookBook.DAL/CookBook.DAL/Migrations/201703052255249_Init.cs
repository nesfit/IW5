namespace CookBook.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IngredientEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecipeEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        Description = c.String(),
                        Duration = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RecipeEntities");
            DropTable("dbo.IngredientEntities");
        }
    }
}
