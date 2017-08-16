namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddServicesToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Service = c.String(nullable: false, maxLength: 50, unicode: false),
                        Level = c.Short(nullable: false),
                        Display = c.Boolean(nullable: false),
                        ParentID = c.Int(),
                        CreatorID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifierID = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Staff", t => t.CreatorID, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.ModifierID, cascadeDelete: false)
                .ForeignKey("dbo.Services", t => t.ParentID)
                .Index(t => t.ParentID)
                .Index(t => t.CreatorID)
                .Index(t => t.ModifierID);
            
            CreateTable(
                "dbo.TeamServices",
                c => new
                    {
                        TeamID = c.Int(nullable: false),
                        ServiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamID, t.ServiceID })
                .ForeignKey("dbo.Teams", t => t.TeamID, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceID, cascadeDelete: true)
                .Index(t => t.TeamID)
                .Index(t => t.ServiceID);
            
            CreateTable(
                "dbo.StaffServices",
                c => new
                    {
                        StaffID = c.Int(nullable: false),
                        ServiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StaffID, t.ServiceID })
                .ForeignKey("dbo.Staff", t => t.StaffID, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceID, cascadeDelete: true)
                .Index(t => t.StaffID)
                .Index(t => t.ServiceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StaffServices", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.StaffServices", "StaffID", "dbo.Staff");
            DropForeignKey("dbo.TeamServices", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.TeamServices", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.Services", "ParentID", "dbo.Services");
            DropForeignKey("dbo.Services", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.Services", "CreatorID", "dbo.Staff");
            DropIndex("dbo.StaffServices", new[] { "ServiceID" });
            DropIndex("dbo.StaffServices", new[] { "StaffID" });
            DropIndex("dbo.TeamServices", new[] { "ServiceID" });
            DropIndex("dbo.TeamServices", new[] { "TeamID" });
            DropIndex("dbo.Services", new[] { "ModifierID" });
            DropIndex("dbo.Services", new[] { "CreatorID" });
            DropIndex("dbo.Services", new[] { "ParentID" });
            DropTable("dbo.StaffServices");
            DropTable("dbo.TeamServices");
            DropTable("dbo.Services");
        }
    }
}
