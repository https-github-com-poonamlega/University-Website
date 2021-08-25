namespace websitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class integration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Service", "Admin_Id", "dbo.Admin");
            DropForeignKey("dbo.Volunteer", "Service_Id", "dbo.Service");
            DropForeignKey("dbo.Volunteer", "User_Id", "dbo.User");
            DropForeignKey("dbo.Events_User_Relation", "Event_Id", "dbo.Events");
            DropIndex("dbo.Service", new[] { "Admin_Id" });
            DropIndex("dbo.Volunteer", new[] { "User_Id" });
            DropIndex("dbo.Volunteer", new[] { "Service_Id" });
            DropIndex("dbo.Events_User_Relation", new[] { "Event_Id" });
            DropTable("dbo.Admin");
            DropTable("dbo.Service");
            DropTable("dbo.Volunteer");
            DropTable("dbo.User");
            DropTable("dbo.Events");
            DropTable("dbo.Events_User_Relation");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Events_User_Relation",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Event_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                        Like_Dislike = c.Int(),
                        Interest = c.Int(),
                        Comment = c.String(),
                        Attendence = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Event_Id = c.Int(nullable: false, identity: true),
                        Event_Name = c.String(nullable: false),
                        Start_Date = c.DateTime(nullable: false),
                        End_Date = c.DateTime(nullable: false),
                        Category = c.String(nullable: false),
                        Participated_User = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Event_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        First_Name = c.String(nullable: false, maxLength: 10),
                        Last_Name = c.String(nullable: false, maxLength: 10),
                        DOB = c.DateTime(nullable: false, storeType: "date"),
                        Gender = c.String(nullable: false, maxLength: 10),
                        Contact_Number = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false, maxLength: 40),
                        Password = c.String(nullable: false, maxLength: 10),
                        A1 = c.String(nullable: false, maxLength: 40),
                        A2 = c.String(nullable: false, maxLength: 40),
                        A3 = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.User_Id);
            
            CreateTable(
                "dbo.Volunteer",
                c => new
                    {
                        Volunteer_Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(nullable: false),
                        Service_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Volunteer_Id);
            
            CreateTable(
                "dbo.Service",
                c => new
                    {
                        Service_Id = c.Int(nullable: false),
                        Admin_Id = c.Int(nullable: false),
                        Service_Name = c.String(nullable: false, maxLength: 20),
                        Service_Description = c.String(nullable: false, maxLength: 100),
                        Reqired_Volunteer = c.Int(nullable: false),
                        Start_Date = c.DateTime(nullable: false, storeType: "date"),
                        Participated_Volunteer = c.Int(),
                    })
                .PrimaryKey(t => t.Service_Id);
            
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        Admin_Id = c.Int(nullable: false),
                        First_Name = c.String(nullable: false, maxLength: 10),
                        Last_Name = c.String(nullable: false, maxLength: 10),
                        DOB = c.DateTime(nullable: false, storeType: "date"),
                        Gender = c.String(nullable: false, maxLength: 10),
                        Contact_Number = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false, maxLength: 40),
                        Password = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Admin_Id);
            
            CreateIndex("dbo.Events_User_Relation", "Event_Id");
            CreateIndex("dbo.Volunteer", "Service_Id");
            CreateIndex("dbo.Volunteer", "User_Id");
            CreateIndex("dbo.Service", "Admin_Id");
            AddForeignKey("dbo.Events_User_Relation", "Event_Id", "dbo.Events", "Event_Id", cascadeDelete: true);
            AddForeignKey("dbo.Volunteer", "User_Id", "dbo.User", "User_Id", cascadeDelete: true);
            AddForeignKey("dbo.Volunteer", "Service_Id", "dbo.Service", "Service_Id", cascadeDelete: true);
            AddForeignKey("dbo.Service", "Admin_Id", "dbo.Admin", "Admin_Id", cascadeDelete: true);
        }
    }
}
