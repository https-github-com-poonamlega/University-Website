namespace websitee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingAdminUserData : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers]
                   ([Id]
                   ,[Email]
                   ,[EmailConfirmed]
                   ,[PasswordHash]
                   ,[SecurityStamp]
                   ,[PhoneNumber]
                   ,[PhoneNumberConfirmed]
                   ,[TwoFactorEnabled]
                   ,[LockoutEndDateUtc]
                   ,[LockoutEnabled]
                   ,[AccessFailedCount]
                   ,[UserName]
                   ,[FirstName]
                   ,[LastName]
                   ,[Dob]
                   ,[Gender]
                   ,[ContactNumber]
                   ,[S1]
                   ,[S2]
                   ,[S3])
             VALUES
                   ('3adbedaf-8b27-4490-9a07-f9abfafa7b4b','admin@gmail.com','False','AM+jgZ8KeONrM3nb9o2SkvMamka925Fsf2FLNIxWQ/GhJ9xr0HnwjWTr7Idv4gV7Fg==','ec0b643a-6b2c-4931-a176-93908c1beccf',NULL,'False','False',NULL,'True',0,'admin@gmail.com','Admin','Admin','2000-10-10 00:00:00.000','Female',7483726357,'Admin','Admin','Admin');

                INSERT INTO [dbo].[AspNetRoles]
                   ([Id]
                   ,[Name])
             VALUES
                   ('18c1e6ef-3b4a-480c-9bb2-c89e27a91e31','IsAdmin');

                INSERT INTO [dbo].[AspNetUserRoles]
                   ([UserId]
                   ,[RoleId])
             VALUES
                   ('3adbedaf-8b27-4490-9a07-f9abfafa7b4b','18c1e6ef-3b4a-480c-9bb2-c89e27a91e31');
            ");
        }
        
        public override void Down()
        {
        }
    }
}
