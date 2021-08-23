namespace websitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminResolutionReWrite : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HelpModels", "AdminResolution", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HelpModels", "AdminResolution", c => c.String(nullable: false));
        }
    }
}
