namespace BloodBankApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bloodApp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.BloodGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.BloodRequests",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        PatientAge = c.Int(nullable: false),
                        Problem = c.String(),
                        Address = c.String(),
                        HospitalName = c.String(),
                        BloodRequestDate = c.DateTime(nullable: false),
                        BloodNeedDate = c.DateTime(nullable: false),
                        Countity = c.Int(nullable: false),
                        Photo = c.Binary(),
                        PhotoPathUrl = c.String(),
                        GroupId = c.Int(nullable: false),
                        GenderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.BloodGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Genders", t => t.GenderId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.GenderId);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        GenderId = c.Int(nullable: false, identity: true),
                        GenderName = c.String(),
                    })
                .PrimaryKey(t => t.GenderId);
            
            CreateTable(
                "dbo.NewRegistrations",
                c => new
                    {
                        RegId = c.Int(nullable: false, identity: true),
                        EmailNumber = c.String(),
                        Password = c.String(),
                        DonorName = c.String(),
                        Phone = c.String(),
                        Age = c.Int(nullable: false),
                        Address = c.String(),
                        Qualification = c.String(),
                        Photo = c.Binary(),
                        ImageUrl = c.String(),
                        GroupId = c.Int(nullable: false),
                        GenderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegId)
                .ForeignKey("dbo.BloodGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Genders", t => t.GenderId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.GenderId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.NewRegistrations", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.NewRegistrations", "GroupId", "dbo.BloodGroups");
            DropForeignKey("dbo.BloodRequests", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.BloodRequests", "GroupId", "dbo.BloodGroups");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.NewRegistrations", new[] { "GenderId" });
            DropIndex("dbo.NewRegistrations", new[] { "GroupId" });
            DropIndex("dbo.BloodRequests", new[] { "GenderId" });
            DropIndex("dbo.BloodRequests", new[] { "GroupId" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.NewRegistrations");
            DropTable("dbo.Genders");
            DropTable("dbo.BloodRequests");
            DropTable("dbo.BloodGroups");
            DropTable("dbo.Admins");
        }
    }
}
