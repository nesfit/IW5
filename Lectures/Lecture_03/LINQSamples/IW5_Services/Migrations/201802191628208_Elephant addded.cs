namespace IW5_Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Elephantaddded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Elephants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TrunkLength = c.Int(nullable: false),
                        Birth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Elephants");
        }
    }
}
