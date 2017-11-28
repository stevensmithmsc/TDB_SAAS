namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeamApprovalstoDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamApprov",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StaffID = c.Int(nullable: false),
                        Team = c.String(nullable: false, maxLength: 100, unicode: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Details = c.String(unicode: false, storeType: "text"),
                        CreatorID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifierID = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Staff", t => t.CreatorID, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.ModifierID, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.StaffID, cascadeDelete: true)
                .Index(t => t.StaffID)
                .Index(t => t.CreatorID)
                .Index(t => t.ModifierID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamApprov", "StaffID", "dbo.Staff");
            DropForeignKey("dbo.TeamApprov", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.TeamApprov", "CreatorID", "dbo.Staff");
            DropIndex("dbo.TeamApprov", new[] { "ModifierID" });
            DropIndex("dbo.TeamApprov", new[] { "CreatorID" });
            DropIndex("dbo.TeamApprov", new[] { "StaffID" });
            DropTable("dbo.TeamApprov");
        }
    }
}
