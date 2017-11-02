namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRATable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RAs",
                c => new
                    {
                        StaffID = c.Int(nullable: false),
                        UUID = c.String(),
                        PDSRoleID = c.Short(),
                        PLUSUpdatedID = c.Short(),
                        ESRUpdatedID = c.Short(),
                        EGifL3 = c.DateTime(),
                        PA1Rec = c.DateTime(),
                        Declaration = c.DateTime(),
                        GoLiveApproved = c.DateTime(),
                        GLALocked = c.Boolean(nullable: false),
                        CHGoLiveAprv = c.DateTime(),
                        CHGLALocked = c.Boolean(nullable: false),
                        AccountCreated = c.DateTime(),
                        AddCITRIX = c.DateTime(),
                        PasswordEmailed = c.DateTime(),
                        AccessToPlus = c.DateTime(),
                        UUIDAddESR = c.DateTime(),
                        FullyCompliant = c.DateTime(),
                        RAComments = c.String(unicode: false, storeType: "text"),
                        CreatorID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifierID = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StaffID)
                .ForeignKey("dbo.Staff", t => t.CreatorID, cascadeDelete: false)
                .ForeignKey("dbo.Status", t => t.ESRUpdatedID)
                .ForeignKey("dbo.Staff", t => t.ModifierID, cascadeDelete: false)
                .ForeignKey("dbo.Status", t => t.PDSRoleID)
                .ForeignKey("dbo.Status", t => t.PLUSUpdatedID)
                .ForeignKey("dbo.Staff", t => t.StaffID)
                .Index(t => t.StaffID)
                .Index(t => t.PDSRoleID)
                .Index(t => t.PLUSUpdatedID)
                .Index(t => t.ESRUpdatedID)
                .Index(t => t.CreatorID)
                .Index(t => t.ModifierID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RAs", "StaffID", "dbo.Staff");
            DropForeignKey("dbo.RAs", "PLUSUpdatedID", "dbo.Status");
            DropForeignKey("dbo.RAs", "PDSRoleID", "dbo.Status");
            DropForeignKey("dbo.RAs", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.RAs", "ESRUpdatedID", "dbo.Status");
            DropForeignKey("dbo.RAs", "CreatorID", "dbo.Staff");
            DropIndex("dbo.RAs", new[] { "ModifierID" });
            DropIndex("dbo.RAs", new[] { "CreatorID" });
            DropIndex("dbo.RAs", new[] { "ESRUpdatedID" });
            DropIndex("dbo.RAs", new[] { "PLUSUpdatedID" });
            DropIndex("dbo.RAs", new[] { "PDSRoleID" });
            DropIndex("dbo.RAs", new[] { "StaffID" });
            DropTable("dbo.RAs");
        }
    }
}
