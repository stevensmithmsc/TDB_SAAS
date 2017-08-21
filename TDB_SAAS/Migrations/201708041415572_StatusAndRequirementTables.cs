namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusAndRequirementTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Req",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StaffID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        StatusID = c.Short(nullable: false),
                        Comments = c.String(unicode: false, storeType: "text"),
                        CreatorID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifierID = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Staff", t => t.CreatorID, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.ModifierID, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.StaffID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusID, cascadeDelete: false)
                .Index(t => t.StaffID)
                .Index(t => t.CourseID)
                .Index(t => t.StatusID)
                .Index(t => t.CreatorID)
                .Index(t => t.ModifierID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        ID = c.Short(nullable: false, identity: true),
                        Status = c.String(nullable: false, maxLength: 50, unicode: false),
                        Requirement = c.Boolean(nullable: false),
                        Attendance = c.Boolean(nullable: false),
                        TNA_OUT = c.Boolean(nullable: false),
                        RA_PDS = c.Boolean(nullable: false),
                        RA_PLUS = c.Boolean(nullable: false),
                        RA_ESR = c.Boolean(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Req", "StatusID", "dbo.Status");
            DropForeignKey("dbo.Status", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.Status", "CreatorID", "dbo.Staff");
            DropForeignKey("dbo.Req", "StaffID", "dbo.Staff");
            DropForeignKey("dbo.Req", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.Req", "CreatorID", "dbo.Staff");
            DropForeignKey("dbo.Req", "CourseID", "dbo.Courses");
            DropIndex("dbo.Status", new[] { "ModifierID" });
            DropIndex("dbo.Status", new[] { "CreatorID" });
            DropIndex("dbo.Req", new[] { "ModifierID" });
            DropIndex("dbo.Req", new[] { "CreatorID" });
            DropIndex("dbo.Req", new[] { "StatusID" });
            DropIndex("dbo.Req", new[] { "CourseID" });
            DropIndex("dbo.Req", new[] { "StaffID" });
            DropTable("dbo.Status");
            DropTable("dbo.Req");
        }
    }
}
