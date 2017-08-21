namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendancesToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StaffID = c.Int(nullable: false),
                        SessID = c.Int(nullable: false),
                        OutcomeID = c.Short(nullable: false),
                        Comments = c.String(unicode: false, storeType: "text"),
                        BookedBy = c.Int(nullable: false),
                        Booked = c.DateTime(nullable: false),
                        Cancelled = c.DateTime(),
                        CancelledBy = c.Int(),
                        ModifierID = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Staff", t => t.BookedBy, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.CancelledBy)
                .ForeignKey("dbo.Staff", t => t.ModifierID, cascadeDelete: false)
                .ForeignKey("dbo.Status", t => t.OutcomeID, cascadeDelete: false)
                .ForeignKey("dbo.Sess", t => t.SessID, cascadeDelete: true)
                .ForeignKey("dbo.Staff", t => t.StaffID, cascadeDelete: true)
                .Index(t => t.StaffID)
                .Index(t => t.SessID)
                .Index(t => t.OutcomeID)
                .Index(t => t.BookedBy)
                .Index(t => t.CancelledBy)
                .Index(t => t.ModifierID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "StaffID", "dbo.Staff");
            DropForeignKey("dbo.Attendances", "SessID", "dbo.Sess");
            DropForeignKey("dbo.Attendances", "OutcomeID", "dbo.Status");
            DropForeignKey("dbo.Attendances", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.Attendances", "CancelledBy", "dbo.Staff");
            DropForeignKey("dbo.Attendances", "BookedBy", "dbo.Staff");
            DropIndex("dbo.Attendances", new[] { "ModifierID" });
            DropIndex("dbo.Attendances", new[] { "CancelledBy" });
            DropIndex("dbo.Attendances", new[] { "BookedBy" });
            DropIndex("dbo.Attendances", new[] { "OutcomeID" });
            DropIndex("dbo.Attendances", new[] { "SessID" });
            DropIndex("dbo.Attendances", new[] { "StaffID" });
            DropTable("dbo.Attendances");
        }
    }
}
