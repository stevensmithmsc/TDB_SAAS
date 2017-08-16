namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCohortsToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cohorts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 10, unicode: false),
                        Cohort = c.String(nullable: false, maxLength: 50, unicode: false),
                        Number = c.Decimal(nullable: false, precision: 3, scale: 1),
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
            
            AddColumn("dbo.Staff", "CohortID", c => c.Int());
            AddColumn("dbo.Teams", "CohortID", c => c.Int());
            CreateIndex("dbo.Staff", "CohortID");
            CreateIndex("dbo.Teams", "CohortID");
            AddForeignKey("dbo.Teams", "CohortID", "dbo.Cohorts", "ID");
            AddForeignKey("dbo.Staff", "CohortID", "dbo.Cohorts", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staff", "CohortID", "dbo.Cohorts");
            DropForeignKey("dbo.Teams", "CohortID", "dbo.Cohorts");
            DropForeignKey("dbo.Cohorts", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.Cohorts", "CreatorID", "dbo.Staff");
            DropIndex("dbo.Cohorts", new[] { "ModifierID" });
            DropIndex("dbo.Cohorts", new[] { "CreatorID" });
            DropIndex("dbo.Teams", new[] { "CohortID" });
            DropIndex("dbo.Staff", new[] { "CohortID" });
            DropColumn("dbo.Teams", "CohortID");
            DropColumn("dbo.Staff", "CohortID");
            DropTable("dbo.Cohorts");
        }
    }
}
