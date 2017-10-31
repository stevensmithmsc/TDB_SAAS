namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTNA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TNAs",
                c => new
                    {
                        StaffID = c.Int(nullable: false),
                        DateReceived = c.DateTime(),
                        TrainerID = c.Int(),
                        ContactDate = c.DateTime(),
                        ContactOutcome = c.String(unicode: false, storeType: "text"),
                        OutcomeID = c.Short(),
                    })
                .PrimaryKey(t => t.StaffID)
                .ForeignKey("dbo.Status", t => t.OutcomeID)
                .ForeignKey("dbo.Staff", t => t.StaffID)
                .ForeignKey("dbo.Staff", t => t.TrainerID)
                .Index(t => t.StaffID)
                .Index(t => t.TrainerID)
                .Index(t => t.OutcomeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TNAs", "TrainerID", "dbo.Staff");
            DropForeignKey("dbo.TNAs", "StaffID", "dbo.Staff");
            DropForeignKey("dbo.TNAs", "OutcomeID", "dbo.Status");
            DropIndex("dbo.TNAs", new[] { "OutcomeID" });
            DropIndex("dbo.TNAs", new[] { "TrainerID" });
            DropIndex("dbo.TNAs", new[] { "StaffID" });
            DropTable("dbo.TNAs");
        }
    }
}
