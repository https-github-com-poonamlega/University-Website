namespace websitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class integration : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.Service_Id)
                .ForeignKey("dbo.Admin", t => t.Admin_Id, cascadeDelete: true)
                .Index(t => t.Admin_Id);
            
            CreateTable(
                "dbo.Volunteer",
                c => new
                    {
                        Volunteer_Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(nullable: false),
                        Service_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Volunteer_Id)
                .ForeignKey("dbo.Service", t => t.Service_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Service_Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .Index(t => t.Event_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events_User_Relation", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Volunteer", "User_Id", "dbo.User");
            DropForeignKey("dbo.Volunteer", "Service_Id", "dbo.Service");
            DropForeignKey("dbo.Service", "Admin_Id", "dbo.Admin");
            DropIndex("dbo.Events_User_Relation", new[] { "Event_Id" });
            DropIndex("dbo.Volunteer", new[] { "Service_Id" });
            DropIndex("dbo.Volunteer", new[] { "User_Id" });
            DropIndex("dbo.Service", new[] { "Admin_Id" });
            DropTable("dbo.Events_User_Relation");
            DropTable("dbo.Events");
            DropTable("dbo.User");
            DropTable("dbo.Volunteer");
            DropTable("dbo.Service");
            DropTable("dbo.Admin");
        }
    }
}
