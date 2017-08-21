namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeamsToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Team = c.String(maxLength: 50, unicode: false),
                        LeaderID = c.Int(),
                        NoTrain = c.Boolean(nullable: false),
                        CreatorID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifierID = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Staff", t => t.CreatorID, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.LeaderID)
                .ForeignKey("dbo.Staff", t => t.ModifierID, cascadeDelete: false)
                .Index(t => t.LeaderID)
                .Index(t => t.CreatorID)
                .Index(t => t.ModifierID);
            
            CreateTable(
                "dbo.TeamMem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StaffID = c.Int(nullable: false),
                        TeamID = c.Int(nullable: false),
                        Main = c.Boolean(nullable: false),
                        Assignment = c.String(maxLength: 12, unicode: false),
                        Active = c.Boolean(nullable: false),
                        CreatorID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifierID = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Staff", t => t.CreatorID, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.ModifierID, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.StaffID, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamID, cascadeDelete: true)
                .Index(t => t.StaffID)
                .Index(t => t.TeamID)
                .Index(t => t.CreatorID)
                .Index(t => t.ModifierID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.TeamMem", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.TeamMem", "StaffID", "dbo.Staff");
            DropForeignKey("dbo.TeamMem", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.TeamMem", "CreatorID", "dbo.Staff");
            DropForeignKey("dbo.Teams", "LeaderID", "dbo.Staff");
            DropForeignKey("dbo.Teams", "CreatorID", "dbo.Staff");
            DropIndex("dbo.TeamMem", new[] { "ModifierID" });
            DropIndex("dbo.TeamMem", new[] { "CreatorID" });
            DropIndex("dbo.TeamMem", new[] { "TeamID" });
            DropIndex("dbo.TeamMem", new[] { "StaffID" });
            DropIndex("dbo.Teams", new[] { "ModifierID" });
            DropIndex("dbo.Teams", new[] { "CreatorID" });
            DropIndex("dbo.Teams", new[] { "LeaderID" });
            DropTable("dbo.TeamMem");
            DropTable("dbo.Teams");
        }
    }
}
