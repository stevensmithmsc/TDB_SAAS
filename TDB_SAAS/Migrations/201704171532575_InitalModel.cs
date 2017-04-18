namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitalModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TitleID = c.Short(),
                        FName = c.String(maxLength: 35, unicode: false),
                        SName = c.String(nullable: false, maxLength: 35, unicode: false),
                        PName = c.String(maxLength: 70, unicode: false),
                        Gender = c.Byte(),
                        JobTitle = c.String(maxLength: 100, unicode: false),
                        LineManID = c.Int(),
                        Phone = c.String(maxLength: 20, unicode: false),
                        Email = c.String(maxLength: 80, unicode: false),
                        ESRID = c.Int(),
                        MName = c.String(maxLength: 35, unicode: false),
                        Comments = c.String(unicode: false, storeType: "text"),
                        CreatorID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifierID = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Staff", t => t.CreatorID)
                .ForeignKey("dbo.Staff", t => t.LineManID)
                .ForeignKey("dbo.Staff", t => t.ModifierID)
                .ForeignKey("dbo.Titles", t => t.TitleID)
                .Index(t => t.TitleID)
                .Index(t => t.LineManID)
                .Index(t => t.CreatorID)
                .Index(t => t.ModifierID);
            
            CreateTable(
                "dbo.Titles",
                c => new
                    {
                        ID = c.Short(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 15, unicode: false),
                        Genders = c.String(maxLength: 5, unicode: false),
                        DefaultGender = c.Byte(),
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
            DropForeignKey("dbo.Staff", "TitleID", "dbo.Titles");
            DropForeignKey("dbo.Titles", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.Titles", "CreatorID", "dbo.Staff");
            DropForeignKey("dbo.Staff", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.Staff", "LineManID", "dbo.Staff");
            DropForeignKey("dbo.Staff", "CreatorID", "dbo.Staff");
            DropIndex("dbo.Titles", new[] { "ModifierID" });
            DropIndex("dbo.Titles", new[] { "CreatorID" });
            DropIndex("dbo.Staff", new[] { "ModifierID" });
            DropIndex("dbo.Staff", new[] { "CreatorID" });
            DropIndex("dbo.Staff", new[] { "LineManID" });
            DropIndex("dbo.Staff", new[] { "TitleID" });
            DropTable("dbo.Titles");
            DropTable("dbo.Staff");
        }
    }
}
