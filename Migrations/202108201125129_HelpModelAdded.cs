namespace websitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HelpModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HelpModels",
                c => new
                    {
                        HelpId = c.Guid(nullable: false, defaultValueSql: "newid()"),
                        Email = c.String(nullable: false),
                        Issue = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DOT = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HelpId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HelpModels");
        }
    }
}
