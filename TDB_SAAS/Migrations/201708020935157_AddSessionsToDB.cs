namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSessionsToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.Short(nullable: false, identity: true),
                        Location = c.String(nullable: false, maxLength: 50, unicode: false),
                        TLoc = c.Boolean(nullable: false),
                        MaxP = c.Short(),
                        Comments = c.String(),
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
                "dbo.Sess",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(),
                        TrainerID = c.Int(),
                        LocationID = c.Short(),
                        Strt = c.DateTime(),
                        Endt = c.DateTime(),
                        MaxP = c.Short(nullable: false),
                        Notes = c.String(unicode: false, storeType: "text"),
                        CreatorID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifierID = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.CourseID)
                .ForeignKey("dbo.Staff", t => t.CreatorID, cascadeDelete: false)
                .ForeignKey("dbo.Locations", t => t.LocationID)
                .ForeignKey("dbo.Staff", t => t.ModifierID, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.TrainerID)
                .Index(t => t.CourseID)
                .Index(t => t.TrainerID)
                .Index(t => t.LocationID)
                .Index(t => t.CreatorID)
                .Index(t => t.ModifierID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sess", "TrainerID", "dbo.Staff");
            DropForeignKey("dbo.Sess", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.Sess", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.Sess", "CreatorID", "dbo.Staff");
            DropForeignKey("dbo.Sess", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Locations", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.Locations", "CreatorID", "dbo.Staff");
            DropIndex("dbo.Sess", new[] { "ModifierID" });
            DropIndex("dbo.Sess", new[] { "CreatorID" });
            DropIndex("dbo.Sess", new[] { "LocationID" });
            DropIndex("dbo.Sess", new[] { "TrainerID" });
            DropIndex("dbo.Sess", new[] { "CourseID" });
            DropIndex("dbo.Locations", new[] { "ModifierID" });
            DropIndex("dbo.Locations", new[] { "CreatorID" });
            DropTable("dbo.Sess");
            DropTable("dbo.Locations");
        }
    }
}
