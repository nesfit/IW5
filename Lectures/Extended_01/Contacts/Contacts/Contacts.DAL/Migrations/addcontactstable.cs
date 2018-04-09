namespace Contacts.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addcontactstable : DbMigration
    {
        public override void Down()
        {
            DropTable("dbo.Contacts");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Firstname = c.String(),
                    Lastname = c.String(),
                    Mail = c.String(),
                    PhoneNumber = c.String(),
                })
                .PrimaryKey(t => t.Id);
        }
    }
}