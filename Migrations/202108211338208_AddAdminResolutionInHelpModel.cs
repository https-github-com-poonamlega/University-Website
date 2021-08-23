namespace websitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdminResolutionInHelpModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HelpModels", "AdminResolution", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HelpModels", "AdminResolution");
        }
    }
}
