namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCostCentreAndSubjective : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CostCentres",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        CCName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Enbld = c.Boolean(nullable: false),
                        CreatorID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifierID = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Code)
                .ForeignKey("dbo.Staff", t => t.CreatorID, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.ModifierID, cascadeDelete: false)
                .Index(t => t.CreatorID)
                .Index(t => t.ModifierID);
            
            CreateTable(
                "dbo.Subjectives",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        SubName = c.String(nullable: false, maxLength: 255, unicode: false),
                        CreatorID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifierID = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Code)
                .ForeignKey("dbo.Staff", t => t.CreatorID, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.ModifierID, cascadeDelete: false)
                .Index(t => t.CreatorID)
                .Index(t => t.ModifierID);
            
            AddColumn("dbo.Staff", "FinanceCode", c => c.Int());
            AddColumn("dbo.Staff", "SubjectiveID", c => c.Int());
            AddColumn("dbo.Teams", "FinanceCode", c => c.Int());
            CreateIndex("dbo.Staff", "FinanceCode");
            CreateIndex("dbo.Staff", "SubjectiveID");
            CreateIndex("dbo.Teams", "FinanceCode");
            AddForeignKey("dbo.Teams", "FinanceCode", "dbo.CostCentres", "Code");
            AddForeignKey("dbo.Staff", "FinanceCode", "dbo.CostCentres", "Code");
            AddForeignKey("dbo.Staff", "SubjectiveID", "dbo.Subjectives", "Code");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staff", "SubjectiveID", "dbo.Subjectives");
            DropForeignKey("dbo.Subjectives", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.Subjectives", "CreatorID", "dbo.Staff");
            DropForeignKey("dbo.Staff", "FinanceCode", "dbo.CostCentres");
            DropForeignKey("dbo.Teams", "FinanceCode", "dbo.CostCentres");
            DropForeignKey("dbo.CostCentres", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.CostCentres", "CreatorID", "dbo.Staff");
            DropIndex("dbo.Subjectives", new[] { "ModifierID" });
            DropIndex("dbo.Subjectives", new[] { "CreatorID" });
            DropIndex("dbo.CostCentres", new[] { "ModifierID" });
            DropIndex("dbo.CostCentres", new[] { "CreatorID" });
            DropIndex("dbo.Teams", new[] { "FinanceCode" });
            DropIndex("dbo.Staff", new[] { "SubjectiveID" });
            DropIndex("dbo.Staff", new[] { "FinanceCode" });
            DropColumn("dbo.Teams", "FinanceCode");
            DropColumn("dbo.Staff", "SubjectiveID");
            DropColumn("dbo.Staff", "FinanceCode");
            DropTable("dbo.Subjectives");
            DropTable("dbo.CostCentres");
        }
    }
}
