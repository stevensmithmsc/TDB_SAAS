namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBoroughsToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boroughs",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Borough = c.String(nullable: false, maxLength: 15, unicode: false),
                        Long = c.String(nullable: false, maxLength: 50, unicode: false),
                        Short = c.String(nullable: false, maxLength: 3, unicode: false),
                        CreatorID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifierID = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Staff", t => t.CreatorID, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.ModifierID, cascadeDelete: false)
                .Index(t => t.CreatorID)
                .Index(t => t.ModifierID);
            
            CreateTable(
                "dbo.TeamBoroughs",
                c => new
                    {
                        TeamID = c.Int(nullable: false),
                        BoroughID = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => new { t.TeamID, t.BoroughID })
                .ForeignKey("dbo.Teams", t => t.TeamID, cascadeDelete: true)
                .ForeignKey("dbo.Boroughs", t => t.BoroughID, cascadeDelete: true)
                .Index(t => t.TeamID)
                .Index(t => t.BoroughID);
            
            CreateTable(
                "dbo.StaffBoroughs",
                c => new
                    {
                        StaffID = c.Int(nullable: false),
                        BoroughID = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => new { t.StaffID, t.BoroughID })
                .ForeignKey("dbo.Staff", t => t.StaffID, cascadeDelete: true)
                .ForeignKey("dbo.Boroughs", t => t.BoroughID, cascadeDelete: true)
                .Index(t => t.StaffID)
                .Index(t => t.BoroughID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StaffBoroughs", "BoroughID", "dbo.Boroughs");
            DropForeignKey("dbo.StaffBoroughs", "StaffID", "dbo.Staff");
            DropForeignKey("dbo.TeamBoroughs", "BoroughID", "dbo.Boroughs");
            DropForeignKey("dbo.TeamBoroughs", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.Boroughs", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.Boroughs", "CreatorID", "dbo.Staff");
            DropIndex("dbo.StaffBoroughs", new[] { "BoroughID" });
            DropIndex("dbo.StaffBoroughs", new[] { "StaffID" });
            DropIndex("dbo.TeamBoroughs", new[] { "BoroughID" });
            DropIndex("dbo.TeamBoroughs", new[] { "TeamID" });
            DropIndex("dbo.Boroughs", new[] { "ModifierID" });
            DropIndex("dbo.Boroughs", new[] { "CreatorID" });
            DropTable("dbo.StaffBoroughs");
            DropTable("dbo.TeamBoroughs");
            DropTable("dbo.Boroughs");
        }
    }
}
