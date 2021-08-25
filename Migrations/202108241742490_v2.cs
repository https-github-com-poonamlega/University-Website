namespace websitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Events_User_Relation");
            AlterColumn("dbo.Events_User_Relation", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Events_User_Relation", "Id");
            DropTable("dbo.GrivananceCreateViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GrivananceCreateViewModels",
                c => new
                    {
                        Grivanance_Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                        Topic = c.String(nullable: false, maxLength: 150),
                        Sub_Topic = c.String(nullable: false, maxLength: 150),
                        Details = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.Grivanance_Id);
            
            DropPrimaryKey("dbo.Events_User_Relation");
            AlterColumn("dbo.Events_User_Relation", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Events_User_Relation", "Id");
        }
    }
}
