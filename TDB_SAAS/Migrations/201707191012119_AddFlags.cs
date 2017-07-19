namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFlags : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Titles", "CreatorID", "dbo.Staff");
            DropForeignKey("dbo.Titles", "ModifierID", "dbo.Staff");
            DropIndex("dbo.Staff", new[] { "CreatorID" });
            DropIndex("dbo.Staff", new[] { "ModifierID" });
            DropIndex("dbo.Titles", new[] { "CreatorID" });
            DropIndex("dbo.Titles", new[] { "ModifierID" });
            CreateTable(
                "dbo.Flags",
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
                "dbo.StaffFlags",
                c => new
                    {
                        StaffID = c.Int(nullable: false),
                        Flag = c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => new { t.StaffID, t.Flag })
                .ForeignKey("dbo.Staff", t => t.StaffID, cascadeDelete: true)
                .ForeignKey("dbo.Flags", t => t.Flag, cascadeDelete: true)
                .Index(t => t.StaffID)
                .Index(t => t.Flag);

            Sql("UPDATE dbo.Staff SET CreatorID = 0 WHERE CreatorID is null");
            Sql("UPDATE dbo.Staff SET ModifierID = 0 WHERE ModifierID is null");
            Sql("UPDATE dbo.Titles SET CreatorID = 0 WHERE CreatorID is null");
            Sql("UPDATE dbo.Titles SET ModifierID = 0 WHERE ModifierID is null");

            AlterColumn("dbo.Staff", "CreatorID", c => c.Int(nullable: false));
            AlterColumn("dbo.Staff", "ModifierID", c => c.Int(nullable: false));
            AlterColumn("dbo.Titles", "Genders", c => c.String(nullable: false, maxLength: 5, unicode: false));
            AlterColumn("dbo.Titles", "CreatorID", c => c.Int(nullable: false));
            AlterColumn("dbo.Titles", "ModifierID", c => c.Int(nullable: false));
            CreateIndex("dbo.Staff", "CreatorID");
            CreateIndex("dbo.Staff", "ModifierID");
            CreateIndex("dbo.Titles", "CreatorID");
            CreateIndex("dbo.Titles", "ModifierID");
            AddForeignKey("dbo.Titles", "CreatorID", "dbo.Staff", "ID", cascadeDelete: false);
            AddForeignKey("dbo.Titles", "ModifierID", "dbo.Staff", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Titles", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.Titles", "CreatorID", "dbo.Staff");
            DropForeignKey("dbo.Flags", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.Flags", "CreatorID", "dbo.Staff");
            DropForeignKey("dbo.StaffFlags", "Flag", "dbo.Flags");
            DropForeignKey("dbo.StaffFlags", "StaffID", "dbo.Staff");
            DropIndex("dbo.StaffFlags", new[] { "Flag" });
            DropIndex("dbo.StaffFlags", new[] { "StaffID" });
            DropIndex("dbo.Titles", new[] { "ModifierID" });
            DropIndex("dbo.Titles", new[] { "CreatorID" });
            DropIndex("dbo.Staff", new[] { "ModifierID" });
            DropIndex("dbo.Staff", new[] { "CreatorID" });
            DropIndex("dbo.Flags", new[] { "ModifierID" });
            DropIndex("dbo.Flags", new[] { "CreatorID" });
            AlterColumn("dbo.Titles", "ModifierID", c => c.Int());
            AlterColumn("dbo.Titles", "CreatorID", c => c.Int());
            AlterColumn("dbo.Titles", "Genders", c => c.String(maxLength: 5, unicode: false));
            AlterColumn("dbo.Staff", "ModifierID", c => c.Int());
            AlterColumn("dbo.Staff", "CreatorID", c => c.Int());
            DropTable("dbo.StaffFlags");
            DropTable("dbo.Flags");
            CreateIndex("dbo.Titles", "ModifierID");
            CreateIndex("dbo.Titles", "CreatorID");
            CreateIndex("dbo.Staff", "ModifierID");
            CreateIndex("dbo.Staff", "CreatorID");
            AddForeignKey("dbo.Titles", "ModifierID", "dbo.Staff", "ID");
            AddForeignKey("dbo.Titles", "CreatorID", "dbo.Staff", "ID");
        }
    }
}
