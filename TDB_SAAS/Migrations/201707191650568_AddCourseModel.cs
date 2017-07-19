namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CFlags",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false),
                        Description = c.String(maxLength: 50, unicode: false),
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
                "dbo.Courses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Course = c.String(maxLength: 80, unicode: false),
                        Length = c.Short(),
                        Notes = c.String(unicode: false, storeType: "text"),
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
                "dbo.CourseFlags",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        Flag = c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => new { t.CourseID, t.Flag })
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.CFlags", t => t.Flag, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.Flag);
            
            CreateTable(
                "dbo.CoursePreReqs",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        PreReqID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseID, t.PreReqID })
                .ForeignKey("dbo.Courses", t => t.CourseID)
                .ForeignKey("dbo.Courses", t => t.PreReqID)
                .Index(t => t.CourseID)
                .Index(t => t.PreReqID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CFlags", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.CFlags", "CreatorID", "dbo.Staff");
            DropForeignKey("dbo.CoursePreReqs", "PreReqID", "dbo.Courses");
            DropForeignKey("dbo.CoursePreReqs", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Courses", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.CourseFlags", "Flag", "dbo.CFlags");
            DropForeignKey("dbo.CourseFlags", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Courses", "CreatorID", "dbo.Staff");
            DropIndex("dbo.CoursePreReqs", new[] { "PreReqID" });
            DropIndex("dbo.CoursePreReqs", new[] { "CourseID" });
            DropIndex("dbo.CourseFlags", new[] { "Flag" });
            DropIndex("dbo.CourseFlags", new[] { "CourseID" });
            DropIndex("dbo.Courses", new[] { "ModifierID" });
            DropIndex("dbo.Courses", new[] { "CreatorID" });
            DropIndex("dbo.CFlags", new[] { "ModifierID" });
            DropIndex("dbo.CFlags", new[] { "CreatorID" });
            DropTable("dbo.CoursePreReqs");
            DropTable("dbo.CourseFlags");
            DropTable("dbo.Courses");
            DropTable("dbo.CFlags");
        }
    }
}
